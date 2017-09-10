# umbraco-translation
Package to simplify creating multilanguage sites using umbraco relations.

## Config
This package supposes that a few conditions are met in order to function properly:
- A new relation type called `translation` is created
- Document type called `site` is present. (This serves as a homepage for specific language version).

## Other packages
We recommend to use Umbraco RelationEditor package, to be able to manually specifiy some translations.
Simple 301 package might come handy as well, in case you need to redirect to default language version right away.

(Nuget and umbraco package installations are in progress..)