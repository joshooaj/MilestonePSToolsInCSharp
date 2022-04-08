param(
  [uri]$ServerAddress,
  [pscredential]$Credential,
  [bool]$BasicUser,
  [string]$OutputPath
)

$connectParams = @{
  ServerAddress = $ServerAddress
  Credential    = $Credential
  BasicUser     = $BasicUser
}
try {
  Connect-ManagementServer @connectParams
  $results = Get-VmsCameraReport
  if (-not [string]::isnullorempty($OutputPath)) {
    $results | Export-Csv -Path $OutputPath -NoTypeInformation
  }
  $results
} finally {
  Disconnect-ManagementServer
}
