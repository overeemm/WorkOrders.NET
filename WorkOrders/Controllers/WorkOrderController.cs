using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkOrders.Controllers.Helpers;
using WorkOrders.Models;

namespace WorkOrders.Controllers
{
  public class WorkOrderController : AsyncController
  {
    // GET: WorkOrder
    [HttpPost]
    public async Task<ActionResult> Save(string from_name, string from_email, WorkOrder order)
    {
      return await SaveInternal(from_name, from_email, order);
    }

    [HttpPost]
    public async Task<ActionResult> SaveIpad(string from_name, string from_email, WorkOrder order)
    {
      return await SaveInternal(from_name, from_email, order);
    }

    private async Task<ActionResult> SaveInternal(string from_name, string from_email, WorkOrder order)
    {
      var cookies = new CookieManager(HttpContext);
      if(cookies.GetUser() == null)
      {
        return RedirectToAction("Index", "Index", new { goed = true });
      }

      PerfectView.PerfectViewSoapClient client = new PerfectView.PerfectViewSoapClient();
      try
      {
        var response = await client.ActivityCreateAsync(Settings.Credentials, new PerfectView.PvActivitySettingsData
        {
          WorkflowId = Settings.SupportWorkflowId,
          WorkflowStepId = Settings.NieuwWorkFlowStepId,
          ParentRelationshipId = Guid.Parse(order.BedrijfsId),
          ChildRelationshipId = !string.IsNullOrEmpty(order.ContactpersoonId) ? Guid.Parse(order.ContactpersoonId) : (Guid?)null,

        }, new PerfectView.PvActivityFollowUpData
        {
          Start = ParseDateTime(order.Momenten.FirstOrDefault(), m => m.Datum, m => m.Begintijd),
          End = ParseDateTime(order.Momenten.FirstOrDefault(), m => m.Datum, m => m.Eindtijd)
        }, new PerfectView.PvFieldValueData[] {
          ActivityRequestBuilder.Build(ActivityFields.Onderwerp, GetOnderwerp(from_name, order.Opdracht) ),
          ActivityRequestBuilder.Build(ActivityFields.Omschrijving, order.Opdracht),
          ActivityRequestBuilder.Build(ActivityFields.Oplossing, BuildOplossing(order)),
        }, null);

        if(response.Body.ActivityCreateResult.Activity == null)
        {
          throw new Exception(response.Body.ActivityCreateResult.ErrorInformation);
        }
        var activityId = response.Body.ActivityCreateResult.Activity.Id;
        await client.ActivityFlowAsync(Settings.Credentials, activityId, Guid.Empty, Guid.Empty, Guid.Parse(order.Wachtrij));

        var sigToImg = new SignatureToImage()
        {
          BackgroundColor = Color.White,
          PenColor = Color.FromArgb(20, 83, 148),
          CanvasWidth = 290,
          CanvasHeight = 330,
          PenWidth = 2,
          FontSize = 24,
          FontName = "Journal",
        };

        using(var signatureImage = sigToImg.SigJsonToImage(order.output))
        using(System.IO.MemoryStream stream = new System.IO.MemoryStream())
        {
          signatureImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
          byte[] imageBytes = stream.ToArray();
          string imageBase64String = Convert.ToBase64String(imageBytes);
          await client.ActivityAddAttachmentAsync(Settings.Credentials, activityId, "handtekening.png", imageBase64String);

          var htmlBytes = System.Text.Encoding.UTF8.GetBytes(ActivityRequestBuilder.BuildHtml(order, imageBase64String));
          string htmlBase64String = Convert.ToBase64String(htmlBytes);
          await client.ActivityAddAttachmentAsync(Settings.Credentials, activityId, "werkbon.html", htmlBase64String);
        }

        return RedirectToAction("Index", "Index", new { goed = true });
      }
      catch(Exception exc)
      {
        return RedirectToAction("Index", "Index", new { goed = false, error = exc.Message });
      }
    }

    private string GetOnderwerp(string fromName, string opdracht)
    {
      var initialen = string.Join("", fromName.Split(' ').Select(p => p.Substring(0, 1)));
      var onderwerp = $"{initialen}:{opdracht}";
      return onderwerp.Substring(0, Math.Min(onderwerp.Length, 255));
    }

    private string BuildOplossing(WorkOrder order)
    {
      var builder = new StringBuilder();
      if(order.Momenten.Any())
      {
        foreach(var datum in order.Momenten.Where(m => !string.IsNullOrEmpty(m.Datum)).Select(m => string.Format("{0} {1}-{2}", m.Datum, m.Begintijd, m.Eindtijd)))
        {
          builder.AppendLine(datum);
        }
        builder.AppendLine();
      }
      if(order.Onderdelen.Any())
      {
        foreach(var datum in order.Onderdelen.Select(o => string.Format("{0} {1}", o.Aantal, o.Omschrijving)))
        {
          builder.AppendLine(datum);
        }
        builder.AppendLine();
      }
      builder.AppendLine(order.Oplossing);
      return builder.ToString();
    }

    private DateTime? ParseDateTime(Moment moment, Func<Moment, string> datumSelector, Func<Moment, string> tijdSelector)
    {
      if(moment != null)
      {
        try
        {
          var datum = datumSelector(moment);
          var tijd = tijdSelector(moment);

          if(datum.Length == 10 && tijd.Length == 5)
          {
            return new DateTime(int.Parse(datum.Substring(0, 4)), int.Parse(datum.Substring(5, 2)), int.Parse(datum.Substring(8, 2)),
              int.Parse(tijd.Substring(0, 2)), int.Parse(tijd.Substring(3, 2)), 0).ToUniversalTime();
          }
        }
        catch
        {
          return null;
        }
      }

      return null;
    }
  }
}