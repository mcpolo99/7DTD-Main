# Path to the AssemblyInfo.cs file
$assemblyInfoPath = "$(ProjectDir)Properties\AssemblyInfo.cs"

# Read the content of the AssemblyInfo.cs file
$assemblyInfoContent = Get-Content $assemblyInfoPath

# Extract the current AssemblyFileVersion
$pattern = '\[assembly: AssemblyFileVersion\("(.*\.)(\d+)\.(\d+)"\)\]'
$currentVersion = $assemblyInfoContent | ForEach-Object {
    if ($_ -match $pattern) {
        $matches[0]
    }
}

# Increment the build number
$buildNumber = $currentVersion -replace $pattern, '$3'
$newBuildNumber = [int]$buildNumber + 1

# Update the AssemblyFileVersion in the content
$newFileVersion = $currentVersion -replace $pattern, "`$1$newBuildNumber.0`""
$updatedContent = $assemblyInfoContent -replace [regex]::Escape($currentVersion), $newFileVersion

# Save the updated content back to the AssemblyInfo.cs file
$updatedContent | Set-Content $assemblyInfoPath
