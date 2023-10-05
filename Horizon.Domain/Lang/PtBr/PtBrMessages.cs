namespace Horizon.Domain.Lang.PtBr;

public abstract class PtBrMessages
{
    // Validation messages.
    public static string Required { get; } = "O campo {0} é obrigatório";
    public static string MinLength { get; } = "O campo {0} deve ter pelo menos {1} caracteres";
    public static string MaxLength { get; } = "O campo {0} deve ter no máximo {1} caracteres";
    public static string Length { get; } = "O campo {0} deve ter {1} caracteres";
    public static string InvalidField { get; } = "O campo {0} está inválido";

    // System error messages.
    public static string InterServerError { get; } = "Erro interno do servidor";
    public static string BadRequest { get; } = "Verifique se todos os campos estão preenchidos corretamente";
    public static string Error { get; } = "Ocorreu um erro, por favor tente novamente mais tarde";
    public static string NotFound { get; } = "{0} não encontrado(a)";
    public static string CreatedFailure { get; } = "Erro ao criar {0}";
    public static string EmailExists { get; set; } = "O e-mail informado já está cadastrado";

    // System success messages.
    public static string CreatedSuccess { get; } = "{0} criado(a) com sucesso";
    public static string UpdatedSuccess { get; } = "{0} atualizado(a) com sucesso";
    public static string DeletedSuccess { get; } = "{0} deletado(a) com sucesso";
}
