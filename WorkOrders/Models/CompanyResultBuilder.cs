using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkOrders.PerfectView;

namespace WorkOrders.Models
{
  public class CompanyResultBuilder
  {
    public static CompanyResult Build(PerfectView.RelationGetResponse response, PerfectView.RelationGetParentRelationshipsResponse parentResponse, PerfectView.RelationGetChildRelationshipsResponse childResponse)
    {
      if(response.Body.RelationGetResult.Relation != null)
      {
        return new CompanyResult
        {
          Name = GetValue(response.Body.RelationGetResult.Relation, RelationField.Organisatienaam),
          Id = parentResponse.Body.RelationGetParentRelationshipsResult.Relationships != null && parentResponse.Body.RelationGetParentRelationshipsResult.Relationships.Any()
                          ? parentResponse.Body.RelationGetParentRelationshipsResult.Relationships[0].Id
                          : Guid.Empty,
          Code = GetValue(response.Body.RelationGetResult.Relation, RelationField.Debiteurnummer),
          Adres = string.Join(" ", GetValue(response.Body.RelationGetResult.Relation, RelationField.Bezoekadresstraat),
                                   GetValue(response.Body.RelationGetResult.Relation, RelationField.Bezoekadresnummer),
                                   GetValue(response.Body.RelationGetResult.Relation, RelationField.Bezoekadrestoevoeging)),
          Postcode = GetValue(response.Body.RelationGetResult.Relation, RelationField.Bezoekadrespostcode),
          Plaats = GetValue(response.Body.RelationGetResult.Relation, RelationField.Bezoekadresplaats),
          Emailadres = GetValue(response.Body.RelationGetResult.Relation, RelationField.Algemeenemailadres),
          Telefoon = GetValue(response.Body.RelationGetResult.Relation, RelationField.Algemeentelefoonnummer),
          Contactpersoon = childResponse.Body.RelationGetChildRelationshipsResult.Relationships != null && childResponse.Body.RelationGetChildRelationshipsResult.Relationships.Any()
                          ? childResponse.Body.RelationGetChildRelationshipsResult.Relationships[0].DisplayName
                          : "",
          ContactpersoonId = childResponse.Body.RelationGetChildRelationshipsResult.Relationships != null && childResponse.Body.RelationGetChildRelationshipsResult.Relationships.Any()
                          ? childResponse.Body.RelationGetChildRelationshipsResult.Relationships[0].Id
                          : (Guid?)null,
          Notities = GetValue(response.Body.RelationGetResult.Relation, RelationField.Extrainfo),
        };
      }
      return null;
    }

    internal static string BuildSettingsGetValue(RelationGetFieldsResponse settingsResponse)
    {
      return string.Join(" ", settingsResponse.Body.RelationGetFieldsResult.Fields.Select(f => string.Format("case RelationField.{1}: return data.FieldValues.Items.First(i => i.Id == Guid.Parse(\"{0}\")).Value;", f.Id, MakeSafe(f.UserFriendlyName))));
    }

    internal static string BuildSettingsEnum(RelationGetFieldsResponse settingsResponse)
    {
      return string.Join(" ", settingsResponse.Body.RelationGetFieldsResult.Fields.Select(f => string.Format("{1},", f.Id, MakeSafe(f.UserFriendlyName))));
    }

    private static string MakeSafe(string input)
    {
      return input.Replace(" ", "").Replace("/", "").Replace("-", "");
    }


    private static string GetValue(WorkOrders.PerfectView.PvRelationData data, RelationField field)
    {
      switch(field)
      {
        case RelationField.OverledenOpgeheven:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("e836ac6a-9f4f-4c88-91ab-1215cfdefa35")).Value;
        case RelationField.Extrainfo:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("52f6cb6a-926f-460d-ab55-3da0a4b67f2a")).Value;
        case RelationField.Sorteernaam:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("47b0e705-0cc4-462d-9399-518db42dc10d")).Value;
        case RelationField.Weergavenaam:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("4427c201-9484-4e63-bb87-598aa25c3c32")).Value;
        case RelationField.Aangemaaktdoor:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("33ca722b-5dcd-43af-a457-6c43e1015851")).Value;
        case RelationField.Aangemaaktop:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("5cd344a1-48a1-4e4f-872b-a6b41549884c")).Value;
        case RelationField.Gewijzigddoor:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("f11bf562-f9a6-4417-8cd7-b3d73bf829e5")).Value;
        case RelationField.Gewijzigdop:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("6899f229-dba6-4d98-a4f1-ba9d37f4af9e")).Value;
        case RelationField.Bezoekadresland:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("dbcb8f1b-803f-4321-abca-0555201e4078")).Value;
        case RelationField.Weblog:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("71256b0b-b2a3-4a86-bc39-06ab249fa045")).Value;
        case RelationField.Postadresstraat:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("e65a46aa-ba7e-4cc6-9ded-1080eba1cc7c")).Value;
        case RelationField.Website:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("997bf4c6-9dd0-4d42-9395-19e0aebadc73")).Value;
        case RelationField.Organisatienaam:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("d1d4d794-61a8-407c-bdd1-1e6827510d20")).Value;
        case RelationField.Postadrestoevoeging:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("6be73d9d-a95f-459d-b7ab-21ae546dc672")).Value;
        case RelationField.NaamKvK:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("dffb850f-ab58-4712-9068-22d1b31f5f98")).Value;
        case RelationField.Vervaldatumidentificatie:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("5187e77e-1243-49db-97ad-2ac510677cea")).Value;
        case RelationField.Factuuradresstraat:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("8efc0788-a13c-404c-9ace-30df9250dd67")).Value;
        case RelationField.Identificatieaanwezig:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("810f91e3-746b-4971-b5aa-33908fa19ca5")).Value;
        case RelationField.Bezoekadrestoevoeging:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("da76f44f-c354-4b6a-8fa6-42985e42eda2")).Value;
        case RelationField.Postadresextra:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("33e229d6-38a1-4e72-a5de-447918854961")).Value;
        case RelationField.Internenaam:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("43477e4c-a3f3-46bd-b126-45116539a6c1")).Value;
        case RelationField.Bezoekadresextra:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("4bdeb408-c29d-4b02-a419-47ede4c7c39f")).Value;
        case RelationField.BankrekeningnummerGironummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("55bc7130-7c27-40d5-a0ad-5043e27d5488")).Value;
        case RelationField.LinkedIn:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("107f4837-7ba2-4967-a38b-606f6b6ee9eb")).Value;
        case RelationField.Twitter:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("075eb39f-0bfa-44c1-9ca8-63d8c7652687")).Value;
        case RelationField.Crediteurnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("fbb17da4-0828-4a27-9093-65079b80b238")).Value;
        case RelationField.Factuuradrestoevoeging:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("85deaaf0-7f57-4d21-b19b-744d0ff046ac")).Value;
        case RelationField.Factuuradresland:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("55da04e8-bfff-46f9-bd00-7729bbd1588d")).Value;
        case RelationField.Postadresplaats:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("510b1bb5-b2d4-4558-8d6e-7d8a621cbf61")).Value;
        case RelationField.Aantalmedewerkers:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("bdd63d7c-b2d3-40c4-addd-7d9cc2f19398")).Value;
        case RelationField.Bedrijfsactiviteit:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("8026c75b-618a-4d7d-ba92-8517416f0781")).Value;
        case RelationField.Extranaam:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("69bb6c4a-b81b-4b3d-ba86-884a0dd36d0a")).Value;
        case RelationField.AfgiftedatuminschrijvingKvK:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("e3a7d5f3-6834-4fb5-a973-896b39fcea23")).Value;
        case RelationField.Bezoekadresplaats:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("b998f8b0-17bb-481f-99ff-8c95782c09a0")).Value;
        case RelationField.Facebook:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("b8d98a5b-c8e1-409b-b4d8-922ed7501f04")).Value;
        case RelationField.Rechtsvorm:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("78827f74-a111-4790-88c2-9274afb65fd5")).Value;
        case RelationField.Relatiecode:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("818569ad-550c-4666-ba66-94d62a1ffc44")).Value;
        case RelationField.Naambank:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("ea1fb638-290b-4ba3-858a-9d07dba47c94")).Value;
        case RelationField.Postadresland:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("bc70a414-0521-442c-b1c4-ae19366e3def")).Value;
        case RelationField.Factuuradresplaats:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("da4754ac-1426-4a1e-860b-ae59df6f2cbd")).Value;
        case RelationField.Algemeenemailadres:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("75970ab4-dad5-4a94-b70f-ae67e7e62575")).Value;
        case RelationField.Debiteurnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("5ca37d6d-d218-40aa-a05a-b32737af0866")).Value;
        case RelationField.Bezoekadrespostcode:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("040130e8-f60f-4534-9783-bcd7ce5db9c6")).Value;
        case RelationField.Postadrespostcode:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("bef9fdcd-aca5-4a9b-9bd6-c66cca68644a")).Value;
        case RelationField.Factuuradrespostcode:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("ac7d2dc3-8fdc-446d-8dc6-ca186bc593df")).Value;
        case RelationField.Algemeentelefoonnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("c8a5397b-3f3d-45a3-ba50-cb2cfa15cd2c")).Value;
        case RelationField.NummerKvK:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("4c4c1b0e-42bb-412d-9c34-ccc9678c2cf1")).Value;
        case RelationField.BTWnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("0eeb1902-7599-4a9e-bc53-cd28cd186ffd")).Value;
        case RelationField.Bezoekadresstraat:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("a3ede6e3-f568-4ea0-82a5-cd656dbb8728")).Value;
        case RelationField.Ibancode:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("f5004863-d334-403e-83c0-cdb2442b8129")).Value;
        case RelationField.Branche:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("7d2f2141-54f4-41a0-9423-ce04cbf46b0e")).Value;
        case RelationField.BICcode:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("773ed094-815c-4dd1-8205-d03ca2ac55e6")).Value;
        case RelationField.Factuuradresextra:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("10f0ee74-ccdc-4b14-87da-dcdd4f85a602")).Value;
        case RelationField.Algemeenfaxnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("93e1264a-f442-4d8a-9369-dcefffb7a932")).Value;
        case RelationField.Factuuradresnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("8f7d1213-1c97-440a-bc65-dcf7dd996b5e")).Value;
        case RelationField.Postadresnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("5e5919da-bd4d-4013-ad9c-feab4ace8f3f")).Value;
        case RelationField.Bezoekadresnummer:
          return data.FieldValues.Items.First(i => i.Id == Guid.Parse("1bb1e06c-c722-40b8-8098-fef591e3cef3")).Value;
      }
      return "";
    }

    public enum RelationField
    {
      OverledenOpgeheven, Extrainfo, Sorteernaam, Weergavenaam, Aangemaaktdoor, Aangemaaktop, Gewijzigddoor, Gewijzigdop, Bezoekadresland, Weblog, Postadresstraat, Website, Organisatienaam, Postadrestoevoeging, NaamKvK, Vervaldatumidentificatie, Factuuradresstraat, Identificatieaanwezig, Bezoekadrestoevoeging, Postadresextra, Internenaam, Bezoekadresextra, BankrekeningnummerGironummer, LinkedIn, Twitter, Crediteurnummer, Factuuradrestoevoeging, Factuuradresland, Postadresplaats, Aantalmedewerkers, Bedrijfsactiviteit, Extranaam, AfgiftedatuminschrijvingKvK, Bezoekadresplaats, Facebook, Rechtsvorm, Relatiecode, Naambank, Postadresland, Factuuradresplaats, Algemeenemailadres, Debiteurnummer, Bezoekadrespostcode, Postadrespostcode, Factuuradrespostcode, Algemeentelefoonnummer, NummerKvK, BTWnummer, Bezoekadresstraat, Ibancode, Branche, BICcode, Factuuradresextra, Algemeenfaxnummer, Factuuradresnummer, Postadresnummer, Bezoekadresnummer,
    }
  }
}