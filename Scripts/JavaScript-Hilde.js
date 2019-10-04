

$(function () {

    // sørger for at man ikke kan velge dato tidligere enn dagens dato
    // 2019-09-29T11:45:15.764Z
    $("#dato").attr("min", new Date().toISOString().split("T")[0]);

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
        var utStreng = "<option value'' disabled selected hidden>Velg stasjon</option>";
        for (var i in stasjon) {
            utStreng += "<option class='dropdown-item' value='" + stasjon[i].StasjonsID + "'>" + stasjon[i].Stasjonsnavn + "</option>";

        }
        $("#fraStasjon").append(utStreng);
    }

    //Legger tilstasjoner i dropdownliste. Hvis man endrer frastasjon, og det er en stasjon man kan reise fra
    //for å komme til allerede valgt tilstasjon, så beholdes verdien i tilstasjon
    function VisTilDropDown(stasjon) {
        var utStreng = "<option value'' disabled selected hidden>Velg stasjon</option>";
        var finnes = false;
        var tilStasjon = parseInt($(".tilStasjon").val())
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
        var utStreng = "<option value'' disabled selected hidden>Velg tidspunkt</option>";
        for (var i in avgang.sort()) {
            utStreng += "<option value='" + avgang[i] + "'>" + avgang[i] + "</option>";
        }
        $("#Tidspunkt").empty();
        $("#Tidspunkt").append(utStreng);
    }
})

function validateTime() {
    var x = document.forms["myForm"]["avgang"].value;
    if (x == "Velg tidspunkt" || x == null) {
        alert("Vennligst velg tidspunkt for avreise");
        return false;
    }
}
function validateFrom() {
    var x = document.forms["myForm"]["fraStasjon"].value;
    if (x == "" || x == "Velg stasjon") {
        alert("Vennligst velg stasjon du reiser fra");
        return false;
    }
}
function validateTo() {
    var x = document.forms["myForm"]["tilStasjon"].value;
    if (x == "" || x == "Velg stasjon") {
        alert("Vennligst velg stasjon du reiser til");
        return false;
    }
}
function validateDate() {
    var x = document.forms["myForm"]["dato"].value;
    if (x == "") {
        alert("Vennligst velg dato for avreise");
        return false;
    }
}

function validateAll() {
    fraOK = validateFrom();
    tilOK = validateTo();
    tidOK = validateTime();
    datoOK = validateDate();
}




$(document).ready(function () {

    $('.count1').prop('disabled', true);
    $(document).on('click', '.plus1', function () {
        var prisVoksen = 53;
        $('.count1').val(parseInt($('.count1').val()) + 1);
        var antVoksen = document.forms["myForm"]["voksen"].value;
        document.getElementById('prisUtVoksen').innerHTML = (antVoksen * prisVoksen) + ',-';

    });

    $(document).on('click', '.minus1', function () {
        var prisVoksen = 53;
        $('.count1').val(parseInt($('.count1').val()) - 1);
        if ($('.count1').val() == -1) {
            $('.count1').val(0);
        }
        var antVoksen = document.forms["myForm"]["voksen"].value;
        document.getElementById('prisUtVoksen').innerHTML = (antVoksen * prisVoksen) + ',-';
    });

    $('.count2').prop('disabled', true);
    $(document).on('click', '.plus2', function () {
        var prisBarn = 32;
        $('.count2').val(parseInt($('.count2').val()) + 1);
        var antBarn = document.forms["myForm"]["barn"].value;
        document.getElementById('prisUtBarn').innerHTML = (antBarn * prisBarn) + ',-';
    });

    $(document).on('click', '.minus2', function () {
        var prisBarn = 32;
        $('.count2').val(parseInt($('.count2').val()) - 1);
        if ($('.count2').val() == -1) {
            $('.count2').val(0);
        }
        var antBarn = document.forms["myForm"]["barn"].value;
        document.getElementById('prisUtBarn').innerHTML = (antBarn * prisBarn) + ',-';
    });

});