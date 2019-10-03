$(function () {

    //gjemmer og viser tur/retur
    $(document).ready(function () {
        $('input[type="radio"]').click(function () {
            if ($(this).attr('id') == 'retur') {
                $('#visReturDato').show();
                $('#visReturTidspunkt').show();
                $('#returDato').attr("required", "true");
                $('#returAvgang').attr("required", "true");
            }
            else {
                document.getElementById("returDato").value = "";
                $('#returDato').removeAttr('required');
                $('#returAvgang').empty('#returAvgang');
                $('#returAvgang').removeAttr('required');
                $('#visReturDato').hide();
                $('#visReturTidspunkt').hide();

            }
        });
    });

    // sørger for at man ikke kan velge dato tidligere enn dagens dato
    // 2019-09-29T11:45:15.764Z
    $("#dato").attr("min", new Date().toISOString().split("T")[0]);

    // sørger for at man ikke kan velge returdato tidligere enn utreisedato
    $("#dato").change(function () {
        var minDate = $("#dato").val();
        $("#returDato").attr("min", minDate);
    });

    //henter alle stasjoner man kan reise fra ved oppstart
    $.ajax({
        url: '/Home/hentFraStasjoner',
        type: 'GET',
        dataType: 'json',
        success: VisFraDropDown,
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });

    //henter stasjoner man kan reise til basert på velgt frastasjon
    $("#fraStasjon").change(function () {
        var id = $("#fraStasjon").val();
        $.ajax({
            url: '/Home/hentTilStasjoner/' + id,
            type: 'GET',
            dataType: 'json',
            success: VisTilDropDown,
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    });

    //Legger frastasjoner i dropdownliste
    function VisFraDropDown(stasjon) {
        var utStreng = "<option value'' selected hidden>Velg stasjon</option>";
        for (var i in stasjon) {
            utStreng += "<option value='" + stasjon[i].StasjonsID + "'>" + stasjon[i].Stasjonsnavn + "</option>";

        }
        $("#fraStasjon").append(utStreng);
    }

    //Legger tilstasjoner i dropdownliste. Hvis man endrer frastasjon, og det er en stasjon man kan reise fra
    //for å komme til allerede valgt tilstasjon, så beholdes verdien i tilstasjon
    function VisTilDropDown(stasjon) {
        var utStreng = "<option value'' selected hidden>Velg stasjon</option>";
        var finnes = false;
        var tilStasjon = parseInt($("#tilStasjon").val())
        for (var i in stasjon) {
            utStreng += "<option value='" + stasjon[i].StasjonsID + "'>" + stasjon[i].Stasjonsnavn + "</option>";
            if (stasjon[i].StasjonsID === tilStasjon) {
                finnes = true;
            }
        }
        $("#tilStasjon").empty();
        $("#tilStasjon").append(utStreng);
        $("#tilStasjon").val(finnes ? tilStasjon : "Velg stasjon");
        VisTidspunkt();
    }

    $("#tilStasjon").change(VisTidspunkt);
    $("#dato").change(VisTidspunkt);

    //legger tidspunkt i dropdownliste. Kjøres ved change både på til, fra og dato, men fullføres kun hvis
    //alle variablene har data
    function VisTidspunkt() {
        var fraStasjon = $("#fraStasjon").val();
        var tilStasjon = $("#tilStasjon").val();
        var dato = $("#dato").val();

        if (fraStasjon && tilStasjon && tilStasjon !== "Velg stasjon" && dato) {
            $.ajax({
                url: '/Home/hentTidspunkt?fraStasjon=' + fraStasjon + "&tilStasjon=" + tilStasjon + "&dato=" + dato,
                type: 'GET',
                dataType: 'json',
                success: VisTidspunktDropDown,
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        } else {
            $("#Tidspunkt").empty();
        }
    }

    function VisTidspunktDropDown(avgang) {
        if (Array.isArray(avgang) && avgang.length) {
            var utStreng = "<option value'' selected hidden>Velg tidspunkt</option>";
            for (var i in avgang.sort()) {
                utStreng += "<option value='" + avgang[i] + "'>" + avgang[i] + "</option>";
            }
            $("#Tidspunkt").empty();
            $("#Tidspunkt").append(utStreng);
        }
        else {
            var utStreng = "<option value'' selected hidden>Ingen flere avganger i dag</option>";
            $("#Tidspunkt").empty();
            $("#Tidspunkt").append(utStreng);
        }
    }


    //Onchange funksjoner for visning av returavgang
    $("#tilStasjon").change(VisReturTidspunkt);
    $("#fraStasjon").change(VisReturTidspunkt);
    $("#Tidspunkt").change(VisReturTidspunkt);
    $("#dato").change(VisReturTidspunkt);
    $("#returDato").change(VisReturTidspunkt);

    //Viser returtidspunkt
    function VisReturTidspunkt() {
        var fraStasjon = $("#tilStasjon").val();
        var tilStasjon = $("#fraStasjon").val();
        var dato = $("#dato").val();
        var returdato = $("#returDato").val();
        var avgang = $("#Tidspunkt").val();

        if (fraStasjon && tilStasjon && tilStasjon !== "Velg stasjon" && fraStasjon !== "Velg stasjon" && dato && returdato && avgang) {
            $.ajax({
                url: '/Home/hentReturTidspunkt?fraStasjon=' + fraStasjon + "&tilStasjon=" + tilStasjon + "&dato=" + dato + "&returdato=" + returdato + "&avgang=" + avgang,
                type: 'GET',
                dataType: 'json',
                success: VisReturTidspunktDropDown,
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        } else {
            $("#returAvgang").empty();
        }
    }

    function VisReturTidspunktDropDown(avgang) {
        if (Array.isArray(avgang) && avgang.length) {
            var utStreng = "<option value'' selected hidden>Velg tidspunkt</option>";
            for (var i in avgang.sort()) {
                utStreng += "<option value='" + avgang[i] + "'>" + avgang[i] + "</option>";
            }
            $("#returAvgang").empty();
            $("#returAvgang").append(utStreng);
        }
        else {
            var utStreng = "<option value'' selected hidden>Ingen flere avganger i dag</option>";
            $("#returAvgang").empty();
            $("#returAvgang").append(utStreng);
        }
    }

})

/* --------------------- klient-side validering på index----------------------*/

/* validerer tidsvalget */
function validateTime() {
    var x = document.forms["myForm"]["avgang"].value;
    if (x == "Velg tidspunkt" || x == null || x == "dd.mm.åååå") {
        alert("Vennligst velg tidspunkt for avreise");
        return false;
    }
    return true;
}
/* validerer frastasjon */
function validateFrom() {
    var x = document.forms["myForm"]["fraStasjon"].value;
    if (x == "" || x == "Velg stasjon") {
        alert("Vennligst velg stasjon du reiser fra");
        return false;
    }
    return true;
}
function validateTo() {
    var x = document.forms["myForm"]["tilStasjon"].value;
    if (x == "" || x == "Velg stasjon") {
        alert("Vennligst velg stasjon du reiser til");
        return false;
    }
    return true;
}
/* validerer datovalg */
function validateDate() {
    var x = document.forms["myForm"]["dato"].value;
    if (x == "") {
        alert("Vennligst velg dato for avreise");
        return false;
    }
    return true;
}
/* validerer returdato */
function validateReturDate() {
    var x = document.forms["myForm"]["returDato"].value;
    if (x == "" || x == "dd.mm.åååå") {
        alert("Vennligst velg returdato for avreise");
        return false;
    }
    return true;
}
/* validerer validerer returtid */
function validateReturTime() {

    var x = document.forms["myForm"]["returAvgang"].value;
    if (x == "Velg tidspunkt" || x == null || x == "") {
        alert("Vennligst velg returtidspunkt for avreise");
        return false;
    }
    return true;
}

/* kjører alle valideringene i feltene ved submit button og stopper sending av skjema ved feil */
function validateAll() {
    var okFra = validateFrom();
    var okTil = validateTo();
    var okTid = validateTime();
    var okDato = validateDate();

    var x = document.forms["myForm"]["tur"].value;
    if (x == "tur/retur" || x == "Tur/Retur" || x == "retur") {
        var okTidRetur = validateReturTime();
        var okDatoRetur = validateReturTime();
        if (okTidRetur && okDatoRetur) {
            return true;
        }
        return false;
    }
    if (okFra && okTil && okTid && okDato) {
        return true;
    }
    return false;
}
/* --------------------- klientvalidering bestilling ---------------*/

/* validerer epost */
function validateEmail() {
    var regEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var OK = regEx.test(document.getElementById("epost").value);
    if (!OK) {
        alert("Vennligst skriv inn en gyldig epostaddresse");
        return false;
    }
    return true;
}