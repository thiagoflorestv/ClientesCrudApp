$(function () {
    
    function carregarClientes() {
        $.get("/api/clientes", function (data) {
            let tbody = $('#tableClientes tbody');
            tbody.empty();

            data.forEach(cliente => {
                let row = `<tr data-id="${cliente.codigoCliente}">
                    <td>${cliente.codigoCliente}</td>
                    <td>${cliente.razaoSocial}</td>
                    <td>${cliente.nomeFantasia}</td>
                    <td>${cliente.documento}</td>
                    <td>${cliente.endereco}</td>
                    <td>${cliente.cidade}</td>
                    <td>${cliente.uf}</td>
                    <td>
                        <a href="/Cliente/Edit/${cliente.codigoCliente}" class="btn btn-sm btn-primary">Editar</a>
                        <button type="button" class="btn btn-sm btn-danger btnExcluir">Excluir</button>
                    </td>
                </tr>`;
                tbody.append(row);
            });
        });
    }
        
    carregarClientes();
    
    $(document).on('click', '.btnExcluir', function () {
        if (!confirm("Deseja realmente excluir este cliente?")) return;

        let row = $(this).closest('tr');
        let codigoCliente = row.data('id');

        $.ajax({
            url: `/api/clientes/${codigoCliente}`,
            type: 'DELETE',
            success: function () {
                row.remove();
                alert("Cliente excluído com sucesso!");
            },
            error: function () {
                alert("Erro ao excluir cliente.");
            }
        });
    });
});
