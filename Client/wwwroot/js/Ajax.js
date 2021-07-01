﻿$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    /*console.log(result);*/
    console.log(result.results);
    text = " ";
    no = 1;
    $.each(result.results, function (key, val) {
        /*text += "<li>" + val.name + "</li>";*/
        text += `<tr>
            <td>${no++}</td>
            <td>${val.name}</td>
            <td>${val.url}</td>
            <td>
                <button type="button" value= "${val.url}"
                    onclick='fillData(this.value)' class="btn btn-success" data-toggle="modal" data-target="#pokeModal">
                    Detail
                </button>
            </td>`;
    });

    $("#showData").html(text);
}).fail((error) => {
    console.log(error);

});

let fillData = (val) => {
    $.ajax({
        url: val
    }).done((result) => {
        srcAbility = " ";
        $.each(result.abilities, function (key, val) {
            srcAbility += (val.ability.name + " ,&nbsp");

        });
            srcHeld = " ";
        $.each(result.types, function (key, val) {
            srcHeld += (val.type.name + " ,&nbsp");
        });
            

        text = `<div class="container">
            <div class="text-center">
                        <img src="${result.sprites.front_shiny}" alt="" /></div>
                                                  

            <div class="row">
                <div class="col">Name       :</div>
                <div class="col">${result.name}</div>
            </div>

            <div class="row">
                <div class="col">Abiity     :</div>
                <div class="col">${srcAbility}</div>
            </div>


            <div class="row">
                <div class="col">Type       :</div>
                <div class="col">${srcHeld}</div>
            </div>

            <div class="row">
                <div class="col">Height     :</div>
                <div class="col">${result.height}</div>
            </div>

            <div class="row">
                <div class="col">Weight     :</div>
                <div class="col">${result.weight}</div>
            </div>
        </div>
        `;

        $("#detailModal").html(text);
    }).fail((error) => {
        console.log(error);
    });    
}















