$MaxParallelThreadsArgument=$args[0]

$XunitRunnerJsonPath = '.\Configuration\AssemblyInfo.cs'
$XunitRunnerJsonRegex = 'LevelOfParallelism\(\d+'
$XunitRunnerJsonReplaceable = 'LevelOfParallelism(' + $MaxParallelThreadsArgument

Write-Host "Updating MaxParallelThreads to $MaxParallelThreadsArgument"

#Get-ChildItem -Path Env:

    if ($MaxParallelThreadsArgument) {

        # Get the xunit.runner.json
        $assemblyInfo = Get-Content -Path $XunitRunnerJsonPath
        Write-Host "File content to update:"
        Write-Host $assemblyInfo

        Write-Host "String to update:" ($assemblyInfo -match $XunitRunnerJsonRegex)

        # Replace number of MaxParallelThreads
        $assemblyInfo = $assemblyInfo -replace
                $XunitRunnerJsonRegex,
                $XunitRunnerJsonReplaceable
        
        Write-Host "Updated string:" ($assemblyInfo -match $XunitRunnerJsonRegex)

        # Writing updated content back to the file
        $assemblyInfo | Set-Content -Path $XunitRunnerJsonPath -Encoding UTF8

        $assemblyInfoUpdated = Get-Content -Path $XunitRunnerJsonPath
        Write-Host "Updated file content:"
        Write-Host $assemblyInfoUpdated

    } else {
        Write-Warning "Max Parallels Threads argument is not passed to PS script. MaxParallelThreads in $XunitRunnerJsonPath is not updated."
    }
