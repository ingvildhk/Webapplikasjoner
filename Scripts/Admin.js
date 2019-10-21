$(function () {

    $.ajax({
        url: '/Admin/hentAlleBanenavn',
        type: 'GET',
        dataType: 'json',
        success: visBanerDropdown,
        error: function (x, y, z) {
            alert();
        }
    });

    function visBanerDropdown(bane) {
        var hidden = 0;
        var utStreng = "<option value = '" + hidden + "' selected hidden>" + "Velg bane" + "</option>";
        for (var i in bane) {
            utStreng += "<option value = '" + bane[i].BaneID + "'>" + bane[i].Banenavn + "</option>";
        }
        $("#BaneID").append(utStreng);
    }


    $.ajax({
        url: '/Admin/hentAlleStasjoner',
        type: 'GET',
        dataType: 'json',
        success: visStasjonerDropdown,
        error: function (x, y, z) {
            alert();
        }
    });

    function visStasjonerDropdown(stasjon) {
        var utStreng = "<option value'' selected hidden>Velg stasjon</option>";
        for (var i in stasjon) {
            utStreng += "<option value = '" + stasjon[i].StasjonID + "'>" + stasjon[i].Stasjonsnavn + "</option>";
        }
        $("#StasjonID").append(utStreng);
    }
});