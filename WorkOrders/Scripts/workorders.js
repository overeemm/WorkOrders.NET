function addProductTypeahead() {

  //$("input.onderdelen[type='text']").typeahead('destroy');

  $("input.onderdelen[type='text']").typeahead({
    source: function (query, process) {
      return $.getJSON('/api/products', { query: query }, function (data) {

        var products = [];
        $.each(data, function (i, p) {
          products.push(p.Name);
        });

        return process(products);
      });
    }
  });
}

function getCompanyInfo(code) {
  $("input[name='BedrijfsCode']").val("");
  $.getJSON('/api/company', { id: code }, function (data) {

    //$("input[name='Bedrijf']").val(data.name);
    $("input[name='Adres']").val(data.Adres);
    $("input[name='Postcode']").val(data.Postcode);
    $("input[name='Plaats']").val(data.Plaats);
    $("input[name='Email']").val(data.Emailadres);
    $("textarea[name='Opdracht']").val(data.Opdracht);
    $("textarea[name='Oplossing']").val(data.Oplossing);
    $("textarea[name='NaamHandtekening']").val(data.NaamHandtekening);
    $("input[name='BedrijfsCode']").val(data.Code);
    $("input[name='Telefoonnummer']").val(data.Telefoon);
    if (data.Contactpersoon) {
      $("input[name='Contactpersoon']").val(data.Contactpersoon);
      $("input[name='ContactpersoonId']").val(data.ContactpersoonId);
    } else {
      $("input[name='Contactpersoon']").val("");
      $("input[name='ContactpersoonId']").val("");
    }
    $("input[name='BedrijfsId']").val(data.Id);
  });
}

function addCompanyTypeahead() {

  //$("input.onderdelen[type='text']").typeahead('destroy');
  var bedrijvenMap = {};
  $("input[name='Bedrijf']").typeahead({
    source: function (query, process) {
      return $.getJSON('/api/companies', { query: query }, function (data) {

        var bedrijven = [];
        $.each(data, function (i, p) {
          bedrijven.push(p.Name);
          bedrijvenMap[p.Name] = p.Id;
        });

        return process(bedrijven);
      });
    },
    updater: function (item) {
      getCompanyInfo(bedrijvenMap[item]);
      return item;
    }
  });
}

function addOnderdeel(index) {
  if (index 
      || $("input.onderdelen[type='number']").last().val() != ""
      || $("input.onderdelen[type='text']").last().val() != "") {

    index = index || new Date().getTime();
    $("<input type='hidden' name='Onderdelen.Index' value='" + index + "' />" +
      "<input class='onderdelen' type='number' name='Onderdelen[" + index + "].Aantal' placeholder='Nr' />" +
      "<input class='onderdelen' type='text' name='Onderdelen[" + index + "].Omschrijving' placeholder='Omschrijving' />" +
      "<div class='clear'> </div>").appendTo($("div.onderdelen"));

    addProductTypeahead();
  }
}

function addMoment(index) {
  
  if (index
      || $("input.momenten[type='date']").last().val() != ""
      || $("input.momenten.start").last().val() != ""
      || $("input.momenten.eind").last().val() != "") {

    index = index || new Date().getTime();
    $("<input type='hidden' name='Momenten.Index' value='" + index + "' />" +
      "<input class='momenten' type='date' name='Momenten[" + index + "].Datum' placeholder='Datum' />" +
      "<input class='momenten start' type='time' name='Momenten[" + index + "].Begintijd' placeholder='08:00' />" +
      "<input class='momenten eind' type='time' class='end' name='Momenten[" + index + "].Eindtijd' placeholder='17:00' />" +
      "<div class='clear'> </div>").appendTo($("div.momenten"));
  }
}

function serializeForm($form) {
  var data = {};
  $form.find("input, textarea").each(function (i, e) {
    var $e = $(e);
    var ename = $e.prop("name");
    if (data[ename]) {
      if (!data[ename].push) {
        var val = data[ename];
        data[ename] = [val];
      }
      data[ename].push($e.val());
    } else {
      data[ename] = $e.val();
    }
  });
  return data;
}

function deserializeForm($form, data) {

  for (var key in data) {
    if (data[key].push) {
      for (var i = 0; i < data[key].length; i++) {
        var val = data[key][i];
        var elem = $form.find("*[name='" + key + "']:eq(" + i + ")");
        if (elem.length == 0) {
          addMoment(val);
          addOnderdeel(val);
          elem = $form.find("*[name='" + key + "']:eq(" + i + ")");
        }
        elem.val(val);
      }
    } else {
      var elem = $form.find("*[name='" + key + "']");
      elem.val(data[key]);
    }
  }
}

function saveForm() {
  var data = serializeForm($("form"));
  window.localStorage.setItem("werkbon", JSON.stringify(data));
}

window.setInterval(saveForm, 500);

$("input[name='from_name']").val("Michiel Overeem");
$("input[name='from_email']").val("overeemm@gmail.com");

$(document).ready(function () {

  $("nav li a").click(function (event) {
    if (!$(this).parent().hasClass("active")) {
      $("div.twelve.columns").hide();
      $("nav li").removeClass("active");
      if ($(this).parent().hasClass("step1")) {
        $("div.twelve.columns.step1").show();
      } else if ($(this).parent().hasClass("step2")) {
        $("div.twelve.columns.step2").show();
      } else if ($(this).parent().hasClass("step3")) {
        $("div.twelve.columns.step3").show();
      } else if ($(this).parent().hasClass("step4")) {
        $("div.twelve.columns.step4").show();
      }
      $(this).parent().addClass("active");
    }
    event.preventDefault();
  });


  addProductTypeahead();
  addCompanyTypeahead();

  var now = new Date();
  var minutes = now.getMinutes();
  var hours = now.getHours();
  var days = now.getDate();
  var months = now.getMonth() + 1;
  while ((minutes % 15) != 0) {
    minutes++;
  }
  if (minutes == 60) {
    minutes = 0;
    hours++;
  }
  if (minutes < 10) { minutes = "00"; }
  if (days < 10) { days = "0" + days; }
  if (months < 10) { months = "0" + months; }
  var currentDate = now.getFullYear() + "-" + months + "-" + days;
  var currentTime = hours + ":" + minutes;

  $('.werkbon').signaturePad({ defaultAction: "drawIt", drawOnly: true, lineWidth: 0 });
  $('.sig').show();

  $("input.momenten[type='date']").first().val(currentDate);
  $("input.momenten.eind").first().val(currentTime);

  $("div.onderdelen").on("change", "input.onderdelen", function (event) {
    addOnderdeel();
  });

  $("div.momenten").on("change", "input.momenten", function (event) {
    addMoment();
  });
});