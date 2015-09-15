using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkOrders.Models
{
  public class WorkOrder
  {
    public string Bedrijf
    { get; set; }
    public string Adres
    { get; set; }
    public string Postcode
    { get; set; }
    public string Plaats
    { get; set; }
    public string Telefoonnummer
    { get; set; }
    public string Contactpersoon
    { get; set; }
    public string Email
    { get; set; }
    public string Referentie
    { get; set; }
    public string Oplossing
    { get; set; }
    public string Opdracht
    { get; set; }
    public string NaamHandtekening
    { get; set; }
    public string Bedrijfscode
    { get; set; }
    public string BedrijfsId
    { get; set; }
    public string ContactpersoonId
    { get; set; }
    public string Wachtrij
    { get; set; }

    /// <summary>
    /// The signature output in JSON
    /// </summary>
    public string output
    { get; set; }

    public Moment[] Momenten
    {
      get;set;
    }

    public Onderdeel[] Onderdelen
    {
      get;set;
    }
  }
}