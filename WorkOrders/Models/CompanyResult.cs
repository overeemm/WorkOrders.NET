using System;

namespace WorkOrders.Models
{
  public class CompanyResult
  {
    public string Name
    { get; set; }
    public Guid Id
    { get; set; }
    public string Code
    { get; set; }
    public string Adres
    { get; set; }
    public string Postcode
    { get; set; }
    public string Plaats
    { get; set; }
    public string Emailadres
    { get; set; }
    public string Telefoon
    { get; set; }
    public string Notities
    { get; set; }
    public string Contactpersoon
    {
      get;
      internal set;
    }
    public Guid? ContactpersoonId
    {
      get;
      internal set;
    }
  }
}