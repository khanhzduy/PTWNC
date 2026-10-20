$word = $null
try {
    $word = New-Object -ComObject Word.Application
    $word.Visible = $false
    $word.DisplayAlerts = 0
    Write-Host "Opening BAO_CAO_BAI_TAP_LON_PTWNC.docx..."
    $doc = $word.Documents.Open("c:\Users\PC\Downloads\PTWNC\BAO_CAO_BAI_TAP_LON_PTWNC.docx")
    Write-Host "Success! Title: " $doc.Name
    $pages = $doc.ComputeStatistics(2)
    Write-Host "Pages: $pages"
    $doc.Close(0)
} catch {
    Write-Host "Caught Exception: $($_.Exception.Message)"
    Write-Host "Stack: $($_.Exception.ToString())"
} finally {
    if ($word -ne $null) {
        $word.Quit()
        [System.Runtime.InteropServices.Marshal]::ReleaseComObject($word) | Out-Null
    }
}
