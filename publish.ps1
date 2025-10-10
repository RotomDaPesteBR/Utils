# Configurações de Script
$ErrorActionPreference = "Stop" # Interrompe o script se ocorrer um erro não tratado
$config = "Release"
$output_dir = "Bin\Packages"
# Lista dos projetos que você deseja empacotar (ajuste estes caminhos conforme suas bibliotecas)
$projects_to_pack = @(
    "Core\Utils.Abstractions\Utils.Abstractions.csproj",
    "Core\Utils.Json\Utils.Json.csproj",
    "Core\Utils.Results\Utils.Results.csproj",
    "Core\Utils\Utils.csproj",
	
    "Web\Utils.AspNet.CORS\Utils.AspNet.CORS.csproj",
    "Web\Utils.AspNet.Results\Utils.AspNet.Results.csproj",
    "Web\Utils.AspNet.OpenAPI\Utils.AspNet.OpenAPI.csproj",
    "Web\Utils.AspNet\Utils.AspNet.csproj",
	
    #"Meta\Utils.Metalama.Factories\Utils.Metalama.Factories.csproj",
    #"Meta\Utils.Metalama.Extensions\Utils.Metalama.Extensions.csproj",
    "Meta\Utils.Metalama\Utils.Metalama.csproj"
)

# --- Limpeza Geral de Artefatos ---
Write-Host "Limpando artefatos de build antigos..." -ForegroundColor Cyan
dotnet clean --configuration $config

# --- Construção (Build) ---
# O dotnet build sem argumentos constrói toda a solução/diretório
Write-Host "`nConstruindo todos os projetos da solução..." -ForegroundColor Yellow
dotnet build --configuration $config /p:ExcludeRestorePackageImports=false /p:IsTestProject=false

# Verifica o resultado do Build
if ($LASTEXITCODE -ne 0) {
    Write-Host "`nERRO: A construção falhou (Exit Code: $LASTEXITCODE). Abortando o empacotamento. Pacotes antigos PRESERVADOS." -ForegroundColor Red
    # Não prossegue para o Read-Host, terminando aqui
    exit 1 
}

# --- Limpeza de Pacotes Existentes ---
# Remove todos os arquivos .nupkg existentes no diretório de saída ANTES de empacotar.
# Esta etapa só é executada se o 'dotnet build' acima for bem-sucedido.
Write-Host "`nBUILD BEM-SUCEDIDO. Excluindo pacotes .nupkg antigos em '$output_dir' para preparar novos..." -ForegroundColor Cyan
if (Test-Path -Path $output_dir -PathType Container) {
    # Procura e exclui todos os arquivos .nupkg dentro do diretório de saída e seus subdiretórios
    $files_to_delete = Get-ChildItem -Path $output_dir -Filter *.nupkg -File -Recurse -ErrorAction SilentlyContinue
    if ($files_to_delete.Count -gt 0) {
        Write-Host "Excluindo $($files_to_delete.Count) pacotes .nupkg existentes..." -ForegroundColor Cyan
        $files_to_delete | Remove-Item -Force
        Write-Host "Limpeza de pacotes concluída." -ForegroundColor Cyan
    } else {
        Write-Host "Nenhum pacote .nupkg existente encontrado para exclusão." -ForegroundColor Cyan
    }
} else {
    Write-Host "Diretório de saída '$output_dir' não existe. Ele será criado durante o 'pack'." -ForegroundColor Cyan
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
