#!/usr/bin/env pwsh

param
(
    [parameter(Mandatory = $true)]
    [string] $ArtifactLocation,

    [parameter(Mandatory = $true)]
    [string] $Configuration,

    [parameter(Mandatory = $false)]
    [string] $VersionSuffix,

    [parameter(Mandatory = $false)]
    [int] $BuildNumber = -1,
    
    [parameter(Mandatory = $false)]
    [string] $DocsPath,
    
    [parameter(Mandatory = $false)]
    [string] $DocsPackageName
)

if ($Configuration -ne "Debug" -and $Configuration -ne "Release")
{
    Write-Host "Invalid configuration specified. Must be Release or Debug."
    Exit 1
}

if (-not $VersionSuffix)
{
    Write-Host "Building production packages"
    & .\rebuild-lib.ps1 -ArtifactLocation "$ArtifactLocation" -Configuration "$Configuration" | Out-Host
}
else
{
    Write-Host "Building pre-production packages"
    if (-not $BuildNumber -or $BuildNumber -lt 0)
    {
        $BuildNumber = -1
    }

    & .\rebuild-lib.ps1 -ArtifactLocation "$ArtifactLocation" -Configuration "$Configuration" -VersionSuffix "$VersionSuffix" -BuildNumber $BuildNumber | Out-Host
}

if ($LastExitCode -ne 0)
{
    Write-Host "Build failed with code $LastExitCode"
    $host.SetShouldExit($LastExitCode)
    Exit $LastExitCode
}

if ($DocsPath -and $DocsPackageName)
{
    Write-Host "Building documentation"
    & .\rebuild-docs.ps1 -DocsPath "$DocsPath" -OutputPath "$ArtifactLocation" -PackageName "$DocsPackageName"
    
    if ($LastExitCode -ne 0)
    {
        Write-Host "Documentation build failed with code $LastExitCode"
        $host.SetShouldExit($LastExitCode)
        Exit $LastExitCode
    }
}
else
{
    Write-Host "Not building documentation"
}

Exit 0
