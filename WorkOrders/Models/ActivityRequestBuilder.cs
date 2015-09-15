using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WorkOrders.PerfectView;

namespace WorkOrders.Models
{
  public class ActivityRequestBuilder
  {
    internal static PvFieldValueData Build(ActivityFields field, string v)
    {
      return new PvFieldValueData()
      {
        Id = GetFieldId(field),
        Value = v
      };
    }

    private static Guid GetFieldId(ActivityFields field)
    {
      switch(field)
      {
        case ActivityFields.Omschrijving:
          return Guid.Parse("d926d3d1-f707-449c-a447-bb8861bec481");
        case ActivityFields.AangemaaktDoor:
          return Guid.Parse("87921403-8efb-4b34-b7d2-6a7a794e7c4b");
        case ActivityFields.Onderwerp:
          return Guid.Parse("411f9719-5342-417d-b529-e02e0dc1a8be");
        case ActivityFields.Oplossing:
          return Guid.Parse("3f2e64d7-d79a-4660-b5f7-bc42dbff547f");
        case ActivityFields.Categorie:
          return Guid.Parse("b819d0fb-5c9f-4c72-8af8-7474ada5eefd");
        case ActivityFields.Urgentie:
          return Guid.Parse("870782d4-7d96-4afc-a4f1-a8fa839e1e70");
        case ActivityFields.Oorzaak:
          return Guid.Parse("aeae80ab-1210-453f-8ae5-72c05ef925a9");
      }
      throw new NotImplementedException();
    }

    

    internal static string BuildHtml(WorkOrder order, string handtekening)
    {
      var builder = new StringBuilder();

      builder.AppendLine("<html>");
      builder.AppendLine("<head>");
      builder.AppendLine("<title></title>");
      builder.AppendLine("</head>");
      builder.AppendLine("<body>");
      builder.AppendLine("<table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"700\">");
      builder.AppendLine("<tbody>");
      builder.AppendLine("<tr><td valign=\"top\" ><table border=\"0\"><tbody><tr>");
      builder.AppendLine("<td colspan=\"2\" style=\"font -weight: normal;\" >");
      builder.AppendLine("Hierbij ontvangt u een digitale werkbon van Overeem Telecom.De werkbon heeft betrekking op de door Overeem Telecom aan u, of aan uw relaties geleverde diensten en / of services.<br/>");
      builder.AppendLine("</td></tr>");
      builder.AppendLine("<tr><td>Opdrachtgever:</td><td><b>");
      builder.AppendLine(order.Bedrijf);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td>Referentie:</td><td><b>");
      builder.AppendLine(order.Referentie);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td>Adres:</td><td><b>");
      builder.AppendLine(order.Adres);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td>&nbsp;</td><td><b>");
      builder.AppendLine(order.Postcode);
      builder.AppendLine(" ");
      builder.AppendLine(order.Plaats);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td>Telefoon:</td><td><b>");
      builder.AppendLine(order.Telefoonnummer);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td>Contactpersoon:</td><td><b>");
      builder.AppendLine(order.Contactpersoon);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td>Email adres:</td><td><b>");
      builder.AppendLine(order.Email);
      builder.AppendLine("</b></td></tr>");

      builder.AppendLine("<tr><td colspan=\"2\"><Hr/></td></tr>");
      builder.AppendLine("<tr><td>Opdracht:</td><td><b>");
      builder.AppendLine(order.Opdracht);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\"><Hr/></td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\">Aankomst en - vertrektijd:</td><td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\">");
      builder.AppendLine(BuildTijden(order.Momenten));
      builder.AppendLine("</td><td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\"><Hr/></td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\">Gebruikte materialen:</td><td></tr><tr><td colspan=\"2\">");
      builder.AppendLine(BuildMaterialen(order.Onderdelen));
      builder.AppendLine("</td><td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\"><Hr/></td></tr>");
      builder.AppendLine("<tr><td>Oplossing:</td><td><b>");
      builder.AppendLine(order.Oplossing);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\"><Hr/></td></tr>");
      builder.AppendLine("<tr><td>Naam:</td><td><b>");
      builder.AppendLine(order.NaamHandtekening);
      builder.AppendLine("</b></td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\">Handtekening voor akkoord:</td><td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\"><img style=\"width: 300px;\" src=\"data:image/png;base64,");
      builder.AppendLine(handtekening);
      builder.AppendLine("\"/></td><td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\"><Hr/></td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\" style=\"color: rgb(255, 128, 0);\">Belangrijke mededeling:</td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\" style=\"font-weight: normal;\">");
      builder.AppendLine("U dient deze digitale werkbon als originele werkbon te beschouwen en deze conform de reglementen van de Belastingdienst zelf uit te printen en toe te voegen aan uw eigen administratie. Mocht u hier niet toe in staat zijn of liever de originele werkbon per post te ontvangen neemt u dan even contact met ons op via het e-mailadres: <a href=\"mailto: info@overeemtelecom.nl\">info@overeemtelecom.nl</a>.</td> ");
      builder.AppendLine("</tr>");
      builder.AppendLine("<tr><td colspan=\"2\" style=\"color: rgb(255, 128, 0);\">Reclamaties:</td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\" style=\"font-weight: normal;\">");
      builder.AppendLine("Indien u fouten constateert in de werkbon, dan dient u deze(binnen 5 dagen) bij Overeem Telecom te reclameren.Reclamaties kunt u melden via het e-mailadres: <a href=\"mailto: info@overeemtelecom.nl\">info@overeemtelecom.nl </a> . De verichtte werkzaamheden overeenkomstig de algemene voorwaarden van Overeem Telecom blijven bestaan.Eventuele verschillen zullen op een nieuwe werkbon worden gecorrigeerd.</td>");
      builder.AppendLine("</tr>");
      builder.AppendLine("<tr><td colspan=\"2\" style=\"color: rgb(255, 128, 0);\">Vragen?</td></tr>");
      builder.AppendLine("<tr><td colspan=\"2\" style=\"font-weight: normal;\">");
      builder.AppendLine("Indien u vragen heeft over deze digitale werkbon kunt u contact opnemen met Overeem Telecom, telefoon:");
      builder.AppendLine("088 - 6837336 of stuur een e-mail naar <a href=\"mailto: planning@overeemtelecom.nl\">planning@overeemtelecom.nl</a></td>");
      builder.AppendLine("</tr>");
      builder.AppendLine("<tr><td colspan=\"2\" style=\"line-height: 11px; font-family: arial,helvetica,sans-serif; color: rgb(170, 170, 170); font-size: 9px; font-weight: normal; text-decoration: none;\"><br />");
      builder.AppendLine("De informatie verzonden met dit e-mailbericht is uitsluitend bestemd voor de geadresseerde. Gebruik van deze informatie door anderen dan de geadresseerde is verboden.Openbaarmaking, vermenigvuldiging, verspreiding en/ of verstrekking van deze informatie aan derden is niet toegestaan.< br />< br />");
      builder.AppendLine("The information contained in this communication is confidential and may be legally privileged.It is intended solely for the use of the individual or entity to whom it is addressed and others authorized to receive it.If you are not the intended recipient, you are hereby notified that any disclosure, copying, distribution, or taking any action in reliance of the contents of this information is strictly prohibited and may be unlawful.Overeem Telecom is neither liable for the proper and & nbsp; complete transmission of the information contained in this communication nor for any delay in its receipt.< br />");
      builder.AppendLine("&nbsp;</td> ");
      builder.AppendLine("</tr>");
      builder.AppendLine("<tr><td align=\"center\" colspan=\"2\" style=\"font-style: italic; font-family: helvetica; color: rgb(255, 128, 0); font-size: 10px;\" valign=\"top\">");
      builder.AppendLine("Overeem Telecom | Eemweg 31 - 24 | &nbsp;");
      builder.AppendLine("3755 LC & nbsp;");
      builder.AppendLine("Eemnes | T. 088 - 6837336 | F. 088 - 6837337 | E. <a href=\"mailto:info@overeemtelecom.nl\">info@overeemtelecom.nl</a></td>");
      builder.AppendLine("</tr>");
      builder.AppendLine("</tbody>");
      builder.AppendLine("</table>");
      builder.AppendLine("</td>");
      builder.AppendLine("</tr>");
      builder.AppendLine("</tbody>");
      builder.AppendLine("</table>");
      builder.AppendLine("<br />");
      builder.AppendLine("</body>");
      builder.AppendLine("</html>");

      return builder.ToString();
    }

    private static string BuildMaterialen(Onderdeel[] onderdelen)
    {
      var builder = new StringBuilder();
      builder.AppendLine("<table>");
      foreach(var datum in onderdelen.Select(o => string.Format("<tr><th>{0}</th><td style=\"text-align: left;\">{1}</td></tr>", o.Aantal, o.Omschrijving)))
      {
        builder.AppendLine(datum);
      }
      builder.AppendLine("</table>");
      return builder.ToString();
    }

    private static string BuildTijden(Moment[] momenten)
    {
      var builder = new StringBuilder();
      builder.AppendLine("<table>");
      foreach(var datum in momenten.Select(m => string.Format("<tr><th style=\"text-align: left;\">{0}</th><td>{1} - {2}</td></tr>", m.Datum, m.Begintijd, m.Eindtijd))){
        builder.AppendLine(datum);
      }
      builder.AppendLine("</table>");
      return builder.ToString();
    }
  }

  public enum ActivityFields
  {
    Omschrijving,
    AangemaaktDoor,
    Onderwerp,
    Oplossing,
    Categorie,
    Urgentie,
    Oorzaak
  }
}