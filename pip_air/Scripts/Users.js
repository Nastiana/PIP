$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Users/GetUsers',
        success: function (responce) {
            var table = $('.table');

            for (let i = 0; i < responce.length; i++) {
                var row = `<tr class="card" id="${responce[i].id}">
                           <td class ="email">${responce[i].email}</td>
                           <td class ="name">${responce[i].userName}</td>
                           <td class ="role">${responce[i].role}</td>
                           <td class ="actions">

                               <a href="/Users/Details/${responce[i].id}" class ="btn btn-default">Детали</a>
                               <a href="/Users/Edit/${responce[i].id}" class ="btn btn-default">Изменить</a>
                               <a class ="btn icon-create" onclick="showDelete('../Users/Delete/${responce[i].id}');">Удалить</a>

                           </td>
                       </tr>`;
                table.find('tbody').append(row);
            }
        },
    });

});