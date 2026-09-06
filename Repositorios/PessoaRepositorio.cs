using MySql.Data.MySqlClient;

public class PessoaRepositorio
{
    // atributo contendo a string de conexao

    private readonly string _stringDeConexao;

    // construtor

    public PessoaRepositorio(string stringDeConexao)
    {
        _stringDeConexao = stringDeConexao;
    }

    // Método para cadastrar
    public Pessoa CadastrarPessoa(Pessoa p)
    {
            // Configurar a conexão
            using var conexao = new MySqlConnection(_stringDeConexao);

            // Realizar a conexão
            conexao.Open();

            // SQL
            string comandoSQL = "INSERT INTO pessoas (nome, cidade, idade) VALUES (@nome, @cidade, @idade);";
            comandoSQL+="SELECT LAST_INSERT_ID();";

            // Variável contendo a ação SQL
            using var comando = new MySqlCommand(comandoSQL, conexao);

            // Informar os parâmetros (nome, cidade e idade)
            comando.Parameters.AddWithValue("@nome", p.Nome);
            comando.Parameters.AddWithValue("@cidade", p.Cidade);
            comando.Parameters.AddWithValue("@idade", p.Idade);

            // Executar comando e retornar o código gerado
            // ExecuteScalar -> Retorna a primeira linha/coluna
            int codigoGerado = Convert.ToInt32(comando.ExecuteScalar());

            // Especificar o código gerado no objeto p
            p.Codigo = codigoGerado;

            // Retorno
            return p;
    }

    // Método para selecionar
    public List<Pessoa> SelecionarPessoas()
    {
            // Cria uma variável chamada pessoas do tipo List<Pessoa>
            List<Pessoa> pessoas = [];

            // Configurar a conexão
            using var conexao = new MySqlConnection(_stringDeConexao);

            // Realizar a conexão
            conexao.Open();

            // Criar comando SQL para seleção de todas as pessoas
            using var comandoSQL = new MySqlCommand("SELECT * FROM pessoas", conexao);
            
            // Executar comando SQL e armazenar todos os registros
            using var registros = comandoSQL.ExecuteReader();
            
            // Laço de repetição
            while (registros.Read())
            {
                // Adicionar cada linha da tabela na variável pessoas
                pessoas.Add(new Pessoa
                {
                    Codigo = registros.GetInt32("codigo"),
                    Nome = registros.GetString("nome"),
                    Cidade = registros.GetString("cidade"),
                    Idade = registros.GetInt32("idade")
                });

            }

            // Retorno
            return pessoas;
    }


        // Método para alterar
    public void AlterarPessoa(Pessoa pessoa)
    {
        // Configurar a conexão
        using var conexao = new MySqlConnection(_stringDeConexao);
        
        // Realizar a conexão
        conexao.Open();

        // Criar comando SQL para alterar dados
        using var comandoSQL = new MySqlCommand("UPDATE pessoas SET nome = @nome, cidade = @cidade, idade = @idade WHERE codigo = @codigo", conexao);
        
        // Especificar os parâmetros do comando SQL
        comandoSQL.Parameters.AddWithValue("@codigo", pessoa.Codigo);
        comandoSQL.Parameters.AddWithValue("@nome", pessoa.Nome);
        comandoSQL.Parameters.AddWithValue("@cidade", pessoa.Cidade);
        comandoSQL.Parameters.AddWithValue("@idade", pessoa.Idade);

        // Executar comando SQL
        comandoSQL.ExecuteNonQuery();
    }

        // Método para remover
    public void RemoverPessoa(int codigo)
    {
        // Configurar a conexão
        using var conexao = new MySqlConnection(_stringDeConexao);

        // Realizar a conexão
        conexao.Open();

        // Criar comando SQL para remover pessoas
        using var comandoSQL = new MySqlCommand("DELETE FROM pessoas WHERE codigo = @codigo", conexao);

        // Especificar o valor do parâmetro código
        comandoSQL.Parameters.AddWithValue("@codigo", codigo);

        // Executar comando SQL
        comandoSQL.ExecuteNonQuery();
    }

        // Método para verificar se a pessoa existe
    public bool ExistePessoa(int codigo)
    {
        // Configurar a conexão
        using var conexao = new MySqlConnection(_stringDeConexao);
        
        // Realizar a conexão
        conexao.Open();

        // Criar comando SQL para verificar a existência da pessoa
        using var comandoSQL = new MySqlCommand("SELECT COUNT(*) FROM pessoas WHERE codigo = @codigo", conexao);
        
        // Especificar o valor do parâmetro código
        comandoSQL.Parameters.AddWithValue("@codigo", codigo);
        
        // Executar comando SQL e retornar o resultado
        int contador = Convert.ToInt32(comandoSQL.ExecuteScalar());
        
        // Retorna verdadeiro se o contador for maior que 0, caso contrário, falso
        return contador > 0;
    }
}