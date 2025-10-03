# Configurações de Script
$ErrorActionPreference = "Stop" # Interrompe o script se ocorrer um erro não tratado
$config = "Release"
$output_dir = "Bin\Packages"
# Lista dos projetos que você deseja empacotar (ajuste estes caminhos conforme suas bibliotecas)
$projects_to_pack = @(
    "Utils\Utils.csproj",
    "Utils.AspNet\Utils.AspNet.csproj",
    "Utils.AspNet.OpenAPI\Utils.AspNet.OpenAPI.csproj",
    "Utils.Metalama\Utils.Metalama.csproj"
)

# --- Limpeza ---
Write-Host "Limpando artefatos antigos..." -ForegroundColor Cyan
dotnet clean --configuration $config

# --- Construção (Build) ---
# O dotnet build sem argumentos constrói toda a solução/diretório
Write-Host "`nConstruindo todos os projetos da solução..." -ForegroundColor Yellow
dotnet build --configuration $config

# Verifica o resultado do Build
if ($LASTEXITCODE -ne 0) {
    Write-Host "`nERRO: A construção falhou (Exit Code: $LASTEXITCODE). Abortando o empacotamento." -ForegroundColor Red
    # Não prossegue para o Read-Host, terminando aqui
    exit 1 
}

# --- Empacotamento (Pack) ---
Write-Host "`nIniciando o empacotamento dos projetos..." -ForegroundColor Yellow

$pack_failure = $false

foreach ($project in $projects_to_pack) {
    Write-Host "`nEmpacotando projeto: $project" -ForegroundColor DarkYellow
    # Usamos --no-build, pois o build completo já foi executado acima
    dotnet pack $project --configuration $config --output $output_dir --no-build
    
    # Verifica o resultado do Pack para cada projeto
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERRO: O empacotamento de '$project' falhou (Exit Code: $LASTEXITCODE)." -ForegroundColor Red
        $pack_failure = $true
    }
}

# Verifica o resultado final do Pack
if ($pack_failure) {
    Write-Host "`nERRO GERAL: Um ou mais projetos falharam no empacotamento." -ForegroundColor Red
} else {
    Write-Host "`nSUCESSO! Todos os pacotes gerados em '$output_dir'" -ForegroundColor Green
}

Read-Host "Pressione Enter para continuar..."
