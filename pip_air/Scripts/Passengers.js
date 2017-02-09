
$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Passengers/GetPassengers',
        success: function (responce) {

            var table = $('.table');
           
            for (let i = 0; i < responce.length; i++) {
                var row = `<tr class="card" Id="${responce[i].Id}">
                           <td class ="Num_flight">${responce[i].Num_flight}</td>
                           <td class ="FIO">${responce[i].FIO}</td>
                           <td class ="Num_pasport">${responce[i].Num_pasport}</td>
                           <td class ="actions">

                               <a href="/Passengers/Edit/${responce[i].Id}" class ="btn btn-default">Изменить</a>
                               <a class ="btn icon-create" onclick="showDelete('../Passengers/Delete/${responce[i].Id}');">Удалить</a>

                           </td>
                       </tr>`;
                table.find('tbody').append(row);
            }
        },
    });

});