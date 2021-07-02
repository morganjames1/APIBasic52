////$.ajax({
////    url: "https://pokeapi.co/api/v2/pokemon"
////}).done((result) => {
////    /*console.log(result);*/
////    console.log(result.results);
////    text = " ";
////    no = 1;
////    $.each(result.results, function (key, val) {
////        /*text += "<li>" + val.name + "</li>";*/
////        text += `<tr>
////            <td>${no++}</td>
////            <td>${val.name}</td>
////            <td>${val.url}</td>
////            <td>
////                <button type="button" value= "${val.url}"
////                    onclick='fillData(this.value)' class="btn btn-success" data-toggle="modal" data-target="#pokeModal">
////                    Detail
////                </button>
////            </td>`;
////    });

////    $("#showData").html(text);
////}).fail((error) => {
////    console.log(error);

////});

////let fillData = (val) => {
////    $.ajax({
////        url: val
////    }).done((result) => {
////        srcAbility = " ";
////        $.each(result.abilities, function (key, val) {
////            srcAbility += (val.ability.name + " &nbsp");

////        });
////            srcHeld = " ";
////        $.each(result.types, function (key, val) {
////            srcHeld += (val.type.name + " &nbsp");
////        });
            

////        text = `<div class="container">
////            <div class="text-center">
////                        <img src="${result.sprites.other.dream_world.front_default}" alt="" /></div>
                                                  

////            <div class="row">
////                <div class="col">Name       </div>
////                <div class="col">: ${result.name}</div>
                
////            </div>

////            <div class="row">
////                <div class="col">Ability     </div>
////                <div class="col">: ${srcAbility}</div>
////            </div>

////            <div class="row">
////                <div class="col">Type       </div>
////                <div class="col">: ${srcHeld}</div>

////            </div>

////            <div class="row">
////                <div class="col">Weight     </div>
////                <div class="col">: ${result.weight} Kg</div>
////            </div>

////            <div class="row">
////                <div class="col">Height     </div>
////                <div class="col">: ${result.height} Cm</div>
////            </div>

           
////        </div>
////        `;

////        $("#detailModal").html(text);
////    }).fail((error) => {
////        console.log(error);
////    });




$(document).ready(function () {
    $('#registerData').DataTable({
        dom: 'Bfrtip',
        buttons: [
            { extend: 'copy'},
            { extend: 'csv'},
            { extend: 'excel'},
            { extend: 'pdf', orientation: 'landscape'},
            { extend: 'print'}          
        ],

        "ajax": {
            url: "https://localhost:44380/Employee/GetRegistrasiView",
            dataType: "json",
            dataSrc: ""

        },
        "columns": [
            {
                "data": "nik"
            },

            {
                "data": "firstName",
                render: function (data, type, row) {
                    return row.firstName + '&nbsp' + row.lastName;
                }
            },
            {
                "data": "phoneNumber",
                render: function (data, type, row) {
                    return "+62" + data.slice(1)
                }
            },
            {
                "data": "gender",
                render: function (data, type, row) {
                    if (data === 0) {
                        return "Male";
                    } else {
                        return "Female";
                    }
                }
            },
            {
                "data": "salary",
                render: function (data, type, row) {
                    return "Rp." + row.salary ;
                }
            },
            {
                "data": "email"
            },
            {
                "data": "degree"
            },
            {
                "data": "gpa"
            },

            {
                "data": null,
                targets: 'no-sort', orderable: false,
                render: function (data, type, row) {
                    return "<button class=\"btn btn-primary\">Edit</button>";
                }
            }
        ]
    });
});





 $("#submit").submit(function (event)
{
     var obj = new Object(); //sesuaikan sendiri nama objek dan isinya
    // ini ngambil value dari inputan dalam form nya

    obj.nik = $("#nik").val();
    obj.firstName = $("#firstname").val();
    obj.lastName = $("#lastname").val();
    obj.phoneNumber = $("#phonenumber").val();
    obj.birthDate = $("#birthdate").val();
    obj.gender = $("#gender").val();
    obj.salary = $("#salary").val();
    obj.email = $("#email").val();
    obj.Password = $("#password").val();
    obj.Degree = $("#degree").val();
    obj.Gpa = $("#gpa").val();
    obj.UniversityId = $("#universityid").val();
    obj.EducationId = $("#educationid").val();

    console.log(obj);

    // isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:44388/API/Employees/Register",
        type: "POST",
        data: JSON.stringify(obj),
        contentType: "application/json",
        dataType: "json"
    }).done((result) => {

       /* $("#reload").ajax.reload(null, false);*/

        alert(result.message);

    }).fail((error) => {

        alert(error.responseJSON.message);

    //alert pemberitahuan jika gagal
})
})























