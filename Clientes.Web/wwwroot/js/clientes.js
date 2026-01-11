var tiposTelefone = [];

// Carrega tipos de telefone via API
function carregarTiposTelefone() {
    return $.get("/api/tipostelefone", function (data) {
        tiposTelefone = data;
    });
}

// Cria nova linha de telefone
function criarLinhaTelefone(tel) {
    tel = tel || {}; 

    var linha = `<tr>
        <td><input name="Telefones[].NumeroTelefone" class="form-control" value="${tel.NumeroTelefone || ''}" /></td>
        <td>
            <select name="Telefones[].CodigoTipoTelefone" class="form-control">
                ${tiposTelefone.map(t => `<option value="${t.codigoTipoTelefone}" ${tel.CodigoTipoTelefone === t.codigoTipoTelefone ? "selected" : ""}>${t.descricaoTipoTelefone}</option>`).join('')}
            </select>
        </td>
        <td><input name="Telefones[].Operadora" class="form-control" value="${tel.Operadora || ''}" /></td>
        <td><input type="checkbox" name="Telefones[].Ativo" ${tel.Ativo !== false ? "checked" : ""} /></td>
        <td><button type="button" class="btn btn-sm btn-danger btnRemove">Remover</button></td>
    </tr>`;

    $('#tableTelefones tbody').append(linha);
}

$(function () {
    // Carrega tipos de telefone
    carregarTiposTelefone().then(() => {
        // Se existir cliente, popula telefones existentes
        if (clienteExistente && clienteExistente.telefones) {
            clienteExistente.telefones.forEach(tel => criarLinhaTelefone(tel));
        } else {
            criarLinhaTelefone(); // adiciona uma linha vazia
        }
    });

    // Adicionar nova linha
    $('#btnAddTelefone').click(() => criarLinhaTelefone());

    // Remover linha
    $(document).on('click', '.btnRemove', function () {
        $(this).closest('tr').remove();
    });

    // Submit do formulário
    $("#formCliente").submit(function (e) {
        e.preventDefault();

        let data = {};
        $(this).serializeArray().forEach(x => {
            if ($(`input[name='${x.name}']`).attr('type') === 'checkbox') {
                data[x.name] = $(`input[name='${x.name}']`).is(':checked');
            } else {
                data[x.name] = x.value;
            }
        });

        // Montar array de telefones
        let telefones = [];
        $('#tableTelefones tbody tr').each(function () {
            let numero = $(this).find("input[name='Telefones[].NumeroTelefone']").val();
            let tipo = $(this).find("select[name='Telefones[].CodigoTipoTelefone']").val();
            let operadora = $(this).find("input[name='Telefones[].Operadora']").val();
            let ativo = $(this).find("input[name='Telefones[].Ativo']").is(":checked");

            if (numero) {
                telefones.push({
                    NumeroTelefone: numero,
                    CodigoTipoTelefone: parseInt(tipo),
                    Operadora: operadora,
                    Ativo: ativo
                });
            }
        });

        data.Telefones = telefones;

        // Validação se é Create ou Edit
        let isEdit = data.CodigoCliente && parseInt(data.CodigoCliente) > 0;
        let url = isEdit ? `/api/clientes/${data.CodigoCliente}` : "/api/clientes";
        let type = isEdit ? "PUT" : "POST";

        $.ajax({
            url: url,
            type: type,
            contentType: "application/json",
            data: JSON.stringify(data),
            success: () => window.location.href = "/Cliente",
            error: () => alert("Erro ao salvar cliente.")
        });
    });
});
