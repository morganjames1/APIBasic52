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


//Datatable and fill table
$(document).ready(function () {
    var table = $('#registerData').DataTable({
        responsive: true,

        dom: 'Bfrtip',
        buttons: [
            { extend: 'copy'},
            { extend: 'csv'},
            { extend: 'excel'},
            { extend: 'pdf', orientation: 'landscape'},
            { extend: 'print'}          
        ],

        "ajax": {
            url: "https://localhost:44388/api/Employees/RegistrasiView",
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
                "data": "name"
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

    //Reload table
    setInterval(function () {
        table.ajax.reload();
    }, 30000);

});

//Insert fill table from form registration to db (create data)
function insert() {
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

    // isi dari object kalian buat sesuai dengan bentuk object yang akan di post (insert)
    $.ajax({
        url: "https://localhost:44388/API/Employees/Register",
        type: "POST",
        data: JSON.stringify(obj),
        contentType: "application/json",
        dataType: "json"
    }).done((result) => {

        /* $("#reload").ajax.reload(null, false);*/

        Swal.fire({
            title: "Good job!",
            text: "Registration Success!",
            icon: "success"
        });


    }).fail((error) => {

        Swal.fire({
            title: "Failed!",
            text: `${error.responseJSON.message}`,
            icon: "Warning"
        });

        //alert pemberitahuan jika gagal
    })
}

// Example starter JavaScript for disabling form submissions if there are invalid fields
window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('needs-validation');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                insert();
            }
            form.classList.add('was-validated');
        });
    }
});


//For fill chart in dashboard link to id

// Chart Gender
let Male = countGender(0);
let Female = countGender(1);

    var optionspie = {
        chart: {
            type: 'donut',
            height: '400px'
        },
        dataLabels: {
            enabled: false
        },
        series: [Male, Female],
        labels: ['male', 'female'],
        noData: {
            text: 'Loading...'
        }
    }

    var chart = new ApexCharts(document.querySelector("#piechart"), optionspie);

    chart.render();

    function countGender(gender) {
        let count = 0;
        jQuery.ajax({
            url: 'https://localhost:44388/api/Employees/RegistrasiView',
            success: function (result) {
                $.each(result, function (key, val) {
                    if (val.gender === gender) {
                        ++count;
                    }
                });
            },
            async: false
        });
        return count;
    }


//Chart University

let uniA = countUniv("Universitas AMIKOM");
let uniB = countUniv("Universitas Telkom");


    var optionsbar = {
        chart: {
            type: 'bar',
            height: '234px'
        },
        series: [{
            name: 'employee from',
            data: [uniA, uniB]
        }],
        xaxis: {
            categories: ["Universitas AMIKOM", "Universitas Telkom"]
        }
    }
    var barChart = new ApexCharts(document.querySelector("#barChart"), optionsbar);
    barChart.render();

    function countUniv(name) {
        let count = 0;
        jQuery.ajax({
            url: 'https://localhost:44388/api/Employees/RegistrasiView',
            success: function (result) {
                $.each(result, function (key, val) {
                    if (val.name === name) {
                        ++count;
                    }
                });
            },
            async: false
        });
        return count;
}

























