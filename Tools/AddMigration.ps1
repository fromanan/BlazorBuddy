param ( [switch]$debug = $false )

$migration = Read-Host "Enter Migration Name"
if ([string]::IsNullOrEmpty("$migration")) {exit}
dotnet ef migrations add $migration

if ($debug -or $lastExitCode)
{
    Write-Host -NoNewLine 'Press any key to continue...';
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
}