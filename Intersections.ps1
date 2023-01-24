$consonants = @('б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ')
$vovels = @('а', 'е', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я')

$idConosant = Get-Random -Minimum 0 -Maximum $consonants.Length-1
if ($idConosant)
{
    
}