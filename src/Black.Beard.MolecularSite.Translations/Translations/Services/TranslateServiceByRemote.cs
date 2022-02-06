using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using DeepL;
using System.Globalization;

namespace Bb.Translations.Services
{

    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(TranslateServiceByRemote), LifeCycle = IocScopeEnum.Transiant)]
    public class TranslateServiceByRemote
    {

        static TranslateServiceByRemote()
        {
            

            //  _dic.Add("", Language);                     // Invariant Language(Invariant Country)
            //  _dic.Add("aa", Language);                   // Afar
            //  _dic.Add("aa-DJ", Language);                // Afar(Djibouti)
            //  _dic.Add("aa-ER", Language);                // Afar(Eritrea)
            //  _dic.Add("aa-ET", Language);                // Afar(Ethiopia)            
            //  _dic.Add("af", Language);                   // Afrikaans
            //  _dic.Add("af-NA", Language);                // Afrikaans(Namibia)
            //  _dic.Add("af-ZA", Language);                // Afrikaans(South Africa)
            //  _dic.Add("agq", Language);                  // Aghem
            //  _dic.Add("agq-CM", Language);               // Aghem(Cameroon)
            //  _dic.Add("ak", Language);                   // Akan
            //  _dic.Add("ak-GH", Language);                // Akan(Ghana)
            //  _dic.Add("am", Language);                   // Amharic
            //  _dic.Add("am-ET", Language);                // Amharic(Ethiopia)
            //  _dic.Add("ar", Language);                   // Arabic
            //  _dic.Add("ar-001", Language);               // Arabic(World)
            //  _dic.Add("ar-AE", Language);                // Arabic(United Arab Emirates)
            //  _dic.Add("ar-BH", Language);                // Arabic(Bahrain)
            //  _dic.Add("ar-DJ", Language);                // Arabic(Djibouti)
            //  _dic.Add("ar-DZ", Language);                // Arabic(Algeria)
            //  _dic.Add("ar-EG", Language);                // Arabic(Egypt)
            //  _dic.Add("ar-ER", Language);                // Arabic(Eritrea)
            //  _dic.Add("ar-IL", Language);                // Arabic(Israel)
            //  _dic.Add("ar-IQ", Language);                // Arabic(Iraq)
            //  _dic.Add("ar-JO", Language);                // Arabic(Jordan)
            //  _dic.Add("ar-KM", Language);                // Arabic(Comoros)
            //  _dic.Add("ar-KW", Language);                // Arabic(Kuwait)
            //  _dic.Add("ar-LB", Language);                // Arabic(Lebanon)
            //  _dic.Add("ar-LY", Language);                // Arabic(Libya)
            //  _dic.Add("ar-MA", Language);                // Arabic(Morocco)
            //  _dic.Add("ar-MR", Language);                // Arabic(Mauritania)
            //  _dic.Add("ar-OM", Language);                // Arabic(Oman)
            //  _dic.Add("ar-PS", Language);                // Arabic(Palestinian Authority)
            //  _dic.Add("ar-QA", Language);                // Arabic(Qatar)
            //  _dic.Add("ar-SA", Language);                // Arabic(Saudi Arabia)
            //  _dic.Add("ar-SD", Language);                // Arabic(Sudan)
            //  _dic.Add("ar-SO", Language);                // Arabic(Somalia)
            //  _dic.Add("ar-SS", Language);                // Arabic(South Sudan)
            //  _dic.Add("ar-SY", Language);                // Arabic(Syria)
            //  _dic.Add("ar-TD", Language);                // Arabic(Chad)
            //  _dic.Add("ar-TN", Language);                // Arabic(Tunisia)
            //  _dic.Add("ar-YE", Language);                // Arabic(Yemen)
            //  _dic.Add("arn", Language);                  // Mapuche
            //  _dic.Add("arn-CL", Language);               // Mapuche(Chile)
            //  _dic.Add("as", Language);                   // Assamese
            //  _dic.Add("as-IN", Language);                // Assamese(India)
            //  _dic.Add("asa", Language);                  // Asu
            //  _dic.Add("asa-TZ", Language);               // Asu(Tanzania)
            //  _dic.Add("ast", Language);                  // Asturian
            //  _dic.Add("ast-ES", Language);               // Asturian(Spain)
            //  _dic.Add("az", Language);                   // Azerbaijani
            //  _dic.Add("az-Cyrl", Language);              // Azerbaijani(Cyrillic)
            //  _dic.Add("az-Cyrl-AZ", Language);           // Azerbaijani(Cyrillic, Azerbaijan)
            //  _dic.Add("az-Latn", Language);              // Azerbaijani(Latin)
            //  _dic.Add("az-Latn-AZ", Language);           // Azerbaijani(Latin, Azerbaijan)
            //  _dic.Add("ba", Language);                   // Bashkir
            //  _dic.Add("ba-RU", Language);                // Bashkir(Russia)
            //  _dic.Add("bas", Language);                  // Basaa
            //  _dic.Add("bas-CM", Language);               // Basaa(Cameroon)
            //  _dic.Add("be", Language);                   // Belarusian
            //  _dic.Add("be-BY", Language);                // Belarusian(Belarus)
            //  _dic.Add("bem", Language);                  // Bemba
            //  _dic.Add("bem-ZM", Language);               // Bemba(Zambia)
            //  _dic.Add("bez", Language);                  // Bena
            //  _dic.Add("bez-TZ", Language);               // Bena(Tanzania)
            _dic.Add("bg", Language.Bulgarian);                   // Bulgarian
            _dic.Add("bg-BG", Language.Bulgarian);                // Bulgarian(Bulgaria)
            // _dic.Add("bm", Language);                   // Bamanankan
            // _dic.Add("bm-ML", Language);                // Bamanankan(Mali)
            // _dic.Add("bn", Language);                   // Bangla
            // _dic.Add("bn-BD", Language);                // Bangla(Bangladesh)
            // _dic.Add("bn-IN", Language);                // Bangla(India)
            // _dic.Add("bo", Language);                   // Tibetan
            // _dic.Add("bo-CN", Language);                // Tibetan(China)
            // _dic.Add("bo-IN", Language);                // Tibetan(India)
            // _dic.Add("br", Language);                   // Breton
            // _dic.Add("br-FR", Language);                // Breton(France)
            // _dic.Add("brx", Language);                  // Bodo
            // _dic.Add("brx-IN", Language);               // Bodo(India)
            // _dic.Add("bs", Language);                   // Bosnian
            // _dic.Add("bs-Cyrl", Language);              // Bosnian(Cyrillic)
            // _dic.Add("bs-Cyrl-BA", Language);           // Bosnian(Cyrillic, Bosnia & Herzegovina)
            // _dic.Add("bs-Latn", Language);              // Bosnian(Latin)
            // _dic.Add("bs-Latn-BA", Language);           // Bosnian(Latin, Bosnia & Herzegovina)
            // _dic.Add("byn", Language);                  // Blin
            // _dic.Add("byn-ER", Language);               // Blin(Eritrea)
            // _dic.Add("ca", Language);                   // Catalan
            // _dic.Add("ca-AD", Language);                // Catalan(Andorra)
            // _dic.Add("ca-ES", Language);                // Catalan(Spain)
            // _dic.Add("ca-ES-VALENCIA", Language);       // Catalan(Spain, Valencian)
            // _dic.Add("ca-FR", Language);                // Catalan(France)
            // _dic.Add("ca-IT", Language);                // Catalan(Italy)
            // _dic.Add("ccp", Language);                  // Chakma
            // _dic.Add("ccp-BD", Language);               // Chakma(Bangladesh)
            // _dic.Add("ccp-IN", Language);               // Chakma(India)
            // _dic.Add("ce", Language);                   // Chechen
            // _dic.Add("ce-RU", Language);                // Chechen(Russia)
            // _dic.Add("ceb", Language);                  // Cebuano
            // _dic.Add("ceb-PH", Language);               // Cebuano(Philippines)
            // _dic.Add("cgg", Language);                  // Chiga
            // _dic.Add("cgg-UG", Language);               // Chiga (Uganda)
            // _dic.Add("chr", Language);                  // Cherokee
            // _dic.Add("chr-US", Language);               // Cherokee (United States)
            // _dic.Add("ckb", Language);                  // Central Kurdish
            // _dic.Add("ckb-IQ", Language);               // Central Kurdish (Iraq)
            // _dic.Add("ckb-IR", Language);               // Central Kurdish (Iran)
            // _dic.Add("co", Language);                   // Corsican
            // _dic.Add("co-FR", Language);                // Corsican (France)
            _dic.Add("cs", Language.Czech);                   // Czech
            _dic.Add("cs-CZ", Language.Czech);                // Czech (Czechia)
            // _dic.Add("cu", Language);                   // Church Slavic
            // _dic.Add("cu-RU", Language);                // Church Slavic (Russia)
            // _dic.Add("cy", Language);                   // Welsh
            // _dic.Add("cy-GB", Language);                // Welsh (United Kingdom)
            _dic.Add("da", Language.Danish);                   // Danish
            _dic.Add("da-DK", Language.Danish);                // Danish (Denmark)
            _dic.Add("da-GL", Language.Danish);                // Danish (Greenland)
            // _dic.Add("dav", Language);                  // Taita
            // _dic.Add("dav-KE", Language);               // Taita (Kenya)
            _dic.Add("de", Language.German);                   // German
            _dic.Add("de-AT", Language.German);                // German (Austria)
            _dic.Add("de-BE", Language.German);                // German (Belgium)
            _dic.Add("de-CH", Language.German);                // German (Switzerland)
            _dic.Add("de-DE", Language.German);                // German (Germany)
            _dic.Add("de-IT", Language.German);                // German (Italy)
            _dic.Add("de-LI", Language.German);                // German (Liechtenstein)
            _dic.Add("de-LU", Language.German);                // German (Luxembourg)
            // _dic.Add("dje", Language);                  // Zarma
            // _dic.Add("dje-NE", Language);               // Zarma (Niger)
            // _dic.Add("dsb", Language);                  // Lower Sorbian
            // _dic.Add("dsb-DE", Language);               // Lower Sorbian (Germany)
            // _dic.Add("dua", Language);                  // Duala
            // _dic.Add("dua-CM", Language);               // Duala (Cameroon)
            // _dic.Add("dv", Language);                   // Divehi
            // _dic.Add("dv-MV", Language);                // Divehi (Maldives)
            // _dic.Add("dyo", Language);                  // Jola-Fonyi
            // _dic.Add("dyo-SN", Language);               // Jola-Fonyi (Senegal)
            // _dic.Add("dz", Language);                   // Dzongkha
            // _dic.Add("dz-BT", Language);                // Dzongkha (Bhutan)
            // _dic.Add("ebu", Language);                  // Embu
            // _dic.Add("ebu-KE", Language);               // Embu (Kenya)
            // _dic.Add("ee", Language);                   // Ewe
            // _dic.Add("ee-GH", Language);                // Ewe (Ghana)
            // _dic.Add("ee-TG", Language);                // Ewe (Togo)
            _dic.Add("el", Language.Greek);                   // Greek
            _dic.Add("el-CY", Language.Greek);                // Greek (Cyprus)
            _dic.Add("el-GR", Language.Greek);                // Greek (Greece)
            _dic.Add("en", Language.English);                   // English
            _dic.Add("en-001", Language.English);               // English (World)
            _dic.Add("en-150", Language.English);               // English (Europe)
            _dic.Add("en-AE", Language.English);                // English (United Arab Emirates)
            _dic.Add("en-AG", Language.English);                // English (Antigua & Barbuda)
            _dic.Add("en-AI", Language.English);                // English (Anguilla)
            _dic.Add("en-AS", Language.English);                // English (American Samoa)
            _dic.Add("en-AT", Language.English);                // English (Austria)
            _dic.Add("en-AU", Language.English);                // English (Australia)
            _dic.Add("en-BB", Language.English);                // English (Barbados)
            _dic.Add("en-BE", Language.English);                // English (Belgium)
            _dic.Add("en-BI", Language.English);                // English (Burundi)
            _dic.Add("en-BM", Language.English);                // English (Bermuda)
            _dic.Add("en-BS", Language.English);                // English (Bahamas)
            _dic.Add("en-BW", Language.English);                // English (Botswana)
            _dic.Add("en-BZ", Language.English);                // English (Belize)
            _dic.Add("en-CA", Language.English);                // English (Canada)
            _dic.Add("en-CC", Language.English);                // English (Cocos [Keeling] Islands)
            _dic.Add("en-CH", Language.English);                // English (Switzerland)
            _dic.Add("en-CK", Language.English);                // English (Cook Islands)
            _dic.Add("en-CM", Language.English);                // English (Cameroon)
            _dic.Add("en-CX", Language.English);                // English (Christmas Island)
            _dic.Add("en-CY", Language.English);                // English (Cyprus)
            _dic.Add("en-DE", Language.English);                // English (Germany)
            _dic.Add("en-DK", Language.English);                // English (Denmark)
            _dic.Add("en-DM", Language.English);                // English (Dominica)
            _dic.Add("en-ER", Language.English);                // English (Eritrea)
            _dic.Add("en-FI", Language.English);                // English (Finland)
            _dic.Add("en-FJ", Language.English);                // English (Fiji)
            _dic.Add("en-FK", Language.English);                // English (Falkland Islands)
            _dic.Add("en-FM", Language.English);                // English (Micronesia)
            _dic.Add("en-GB", Language.BritishEnglish);                // English (United Kingdom)
            _dic.Add("en-GD", Language.English);                // English (Grenada)
            _dic.Add("en-GG", Language.English);                // English (Guernsey)
            _dic.Add("en-GH", Language.English);                // English (Ghana)
            _dic.Add("en-GI", Language.English);                // English (Gibraltar)
            _dic.Add("en-GM", Language.English);                // English (Gambia)
            _dic.Add("en-GU", Language.English);                // English (Guam)
            _dic.Add("en-GY", Language.English);                // English (Guyana)
            _dic.Add("en-HK", Language.English);                // English (Hong Kong SAR)
            _dic.Add("en-IE", Language.English);                // English (Ireland)
            _dic.Add("en-IL", Language.English);                // English (Israel)
            _dic.Add("en-IM", Language.English);                // English (Isle of Man)
            _dic.Add("en-IN", Language.English);                // English (India)
            _dic.Add("en-IO", Language.English);                // English (British Indian Ocean Territory)
            _dic.Add("en-JE", Language.English);                // English (Jersey)
            _dic.Add("en-JM", Language.English);                // English (Jamaica)
            _dic.Add("en-KE", Language.English);                // English (Kenya)
            _dic.Add("en-KI", Language.English);                // English (Kiribati)
            _dic.Add("en-KN", Language.English);                // English (St. Kitts & Nevis)
            _dic.Add("en-KY", Language.English);                // English (Cayman Islands)
            _dic.Add("en-LC", Language.English);                // English (St. Lucia)
            _dic.Add("en-LR", Language.English);                // English (Liberia)
            _dic.Add("en-LS", Language.English);                // English (Lesotho)
            _dic.Add("en-MG", Language.English);                // English (Madagascar)
            _dic.Add("en-MH", Language.English);                // English (Marshall Islands)
            _dic.Add("en-MO", Language.English);                // English (Macao SAR)
            _dic.Add("en-MP", Language.English);                // English (Northern Mariana Islands)
            _dic.Add("en-MS", Language.English);                // English (Montserrat)
            _dic.Add("en-MT", Language.English);                // English (Malta)
            _dic.Add("en-MU", Language.English);                // English (Mauritius)
            _dic.Add("en-MW", Language.English);                // English (Malawi)
            _dic.Add("en-MY", Language.English);                // English (Malaysia)
            _dic.Add("en-NA", Language.English);                // English (Namibia)
            _dic.Add("en-NF", Language.English);                // English (Norfolk Island)
            _dic.Add("en-NG", Language.English);                // English (Nigeria)
            _dic.Add("en-NL", Language.English);                // English (Netherlands)
            _dic.Add("en-NR", Language.English);                // English (Nauru)
            _dic.Add("en-NU", Language.English);                // English (Niue)
            _dic.Add("en-NZ", Language.English);                // English (New Zealand)
            _dic.Add("en-PG", Language.English);                // English (Papua New Guinea)
            _dic.Add("en-PH", Language.English);                // English (Philippines)
            _dic.Add("en-PK", Language.English);                // English (Pakistan)
            _dic.Add("en-PN", Language.English);                // English (Pitcairn Islands)
            _dic.Add("en-PR", Language.English);                // English (Puerto Rico)
            _dic.Add("en-PW", Language.English);                // English (Palau)
            _dic.Add("en-RW", Language.English);                // English (Rwanda)
            _dic.Add("en-SB", Language.English);                // English (Solomon Islands)
            _dic.Add("en-SC", Language.English);                // English (Seychelles)
            _dic.Add("en-SD", Language.English);                // English (Sudan)
            _dic.Add("en-SE", Language.English);                // English (Sweden)
            _dic.Add("en-SG", Language.English);                // English (Singapore)
            _dic.Add("en-SH", Language.English);                // English (St Helena, Ascension, Tristan da Cunha)
            _dic.Add("en-SI", Language.English);                // English (Slovenia)
            _dic.Add("en-SL", Language.English);                // English (Sierra Leone)
            _dic.Add("en-SS", Language.English);                // English (South Sudan)
            _dic.Add("en-SX", Language.English);                // English (Sint Maarten)
            _dic.Add("en-SZ", Language.English);                // English (Eswatini)
            _dic.Add("en-TC", Language.English);                // English (Turks & Caicos Islands)
            _dic.Add("en-TK", Language.English);                // English (Tokelau)
            _dic.Add("en-TO", Language.English);                // English (Tonga)
            _dic.Add("en-TT", Language.English);                // English (Trinidad & Tobago)
            _dic.Add("en-TV", Language.English);                // English (Tuvalu)
            _dic.Add("en-TZ", Language.English);                // English (Tanzania)
            _dic.Add("en-UG", Language.English);                // English (Uganda)
            _dic.Add("en-UM", Language.English);                // English (U.S. Outlying Islands)
            _dic.Add("en-US", Language.AmericanEnglish);                // English (United States)
            _dic.Add("en-US-POSIX", Language.English);          // English (United States, Computer)
            _dic.Add("en-VC", Language.English);                // English (St. Vincent & Grenadines)
            _dic.Add("en-VG", Language.English);                // English (British Virgin Islands)
            _dic.Add("en-VI", Language.English);                // English (U.S. Virgin Islands)
            _dic.Add("en-VU", Language.English);                // English (Vanuatu)
            _dic.Add("en-WS", Language.English);                // English (Samoa)
            _dic.Add("en-ZA", Language.English);                // English (South Africa)
            _dic.Add("en-ZM", Language.English);                // English (Zambia)
            _dic.Add("en-ZW", Language.English);                // English (Zimbabwe)
            // _dic.Add("eo", Language);                   // Esperanto
            // _dic.Add("eo-001", Language);               // Esperanto (World)
            _dic.Add("es", Language.Spanish);                   // Spanish
            _dic.Add("es-419", Language.Spanish);               // Spanish (Latin America)
            _dic.Add("es-AR", Language.Spanish);                // Spanish (Argentina)
            _dic.Add("es-BO", Language.Spanish);                // Spanish (Bolivia)
            _dic.Add("es-BR", Language.Spanish);                // Spanish (Brazil)
            _dic.Add("es-BZ", Language.Spanish);                // Spanish (Belize)
            _dic.Add("es-CL", Language.Spanish);                // Spanish (Chile)
            _dic.Add("es-CO", Language.Spanish);                // Spanish (Colombia)
            _dic.Add("es-CR", Language.Spanish);                // Spanish (Costa Rica)
            _dic.Add("es-CU", Language.Spanish);                // Spanish (Cuba)
            _dic.Add("es-DO", Language.Spanish);                // Spanish (Dominican Republic)
            _dic.Add("es-EC", Language.Spanish);                // Spanish (Ecuador)
            _dic.Add("es-ES", Language.Spanish);                // Spanish (Spain)
            _dic.Add("es-GQ", Language.Spanish);                // Spanish (Equatorial Guinea)
            _dic.Add("es-GT", Language.Spanish);                // Spanish (Guatemala)
            _dic.Add("es-HN", Language.Spanish);                // Spanish (Honduras)
            _dic.Add("es-MX", Language.Spanish);                // Spanish (Mexico)
            _dic.Add("es-NI", Language.Spanish);                // Spanish (Nicaragua)
            _dic.Add("es-PA", Language.Spanish);                // Spanish (Panama)
            _dic.Add("es-PE", Language.Spanish);                // Spanish (Peru)
            _dic.Add("es-PH", Language.Spanish);                // Spanish (Philippines)
            _dic.Add("es-PR", Language.Spanish);                // Spanish (Puerto Rico)
            _dic.Add("es-PY", Language.Spanish);                // Spanish (Paraguay)
            _dic.Add("es-SV", Language.Spanish);                // Spanish (El Salvador)
            _dic.Add("es-US", Language.Spanish);                // Spanish (United States)
            _dic.Add("es-UY", Language.Spanish);                // Spanish (Uruguay)
            _dic.Add("es-VE", Language.Spanish);                // Spanish (Venezuela)
            _dic.Add("et", Language.Estonian);                   // Estonian
            _dic.Add("et-EE", Language.Estonian);                // Estonian (Estonia)
            // _dic.Add("eu", Language);                   // Basque
            // _dic.Add("eu-ES", Language);                // Basque (Spain)
            // _dic.Add("ewo", Language);                  // Ewondo
            // _dic.Add("ewo-CM", Language);               // Ewondo (Cameroon)
            // _dic.Add("fa", Language);                   // Persian
            // _dic.Add("fa-AF", Language);                // Persian (Afghanistan)
            // _dic.Add("fa-IR", Language);                // Persian (Iran)
            // _dic.Add("ff", Language);                   // Fulah
            // _dic.Add("ff-Latn", Language);              // Fulah (Latin)
            // _dic.Add("ff-Latn-BF", Language);           // Fulah (Latin, Burkina Faso)
            // _dic.Add("ff-Latn-CM", Language);           // Fulah (Latin, Cameroon)
            // _dic.Add("ff-Latn-GH", Language);           // Fulah (Latin, Ghana)
            // _dic.Add("ff-Latn-GM", Language);           // Fulah (Latin, Gambia)
            // _dic.Add("ff-Latn-GN", Language);           // Fulah (Latin, Guinea)
            // _dic.Add("ff-Latn-GW", Language);           // Fulah (Latin, Guinea-Bissau)
            // _dic.Add("ff-Latn-LR", Language);           // Fulah (Latin, Liberia)
            // _dic.Add("ff-Latn-MR", Language);           // Fulah (Latin, Mauritania)
            // _dic.Add("ff-Latn-NE", Language);           // Fulah (Latin, Niger)
            // _dic.Add("ff-Latn-NG", Language);           // Fulah (Latin, Nigeria)
            // _dic.Add("ff-Latn-SL", Language);           // Fulah (Latin, Sierra Leone)
            // _dic.Add("ff-Latn-SN", Language);           // Fulah (Latin, Senegal)
            _dic.Add("fi", Language.Finnish);                   // Finnish
            _dic.Add("fi-FI", Language.Finnish);                // Finnish (Finland)
            // _dic.Add("fil", Language);                  // Filipino
            // _dic.Add("fil-PH", Language);               // Filipino (Philippines)
            // _dic.Add("fo", Language);                   // Faroese
            // _dic.Add("fo-DK", Language);                // Faroese (Denmark)
            // _dic.Add("fo-FO", Language);                // Faroese (Faroe Islands)
            _dic.Add("fr", Language.French);                   // French
            _dic.Add("fr-BE", Language.French);                // French (Belgium)
            _dic.Add("fr-BF", Language.French);                // French (Burkina Faso)
            _dic.Add("fr-BI", Language.French);                // French (Burundi)
            _dic.Add("fr-BJ", Language.French);                // French (Benin)
            _dic.Add("fr-BL", Language.French);                // French (St. Barthélemy)
            _dic.Add("fr-CA", Language.French);                // French (Canada)
            _dic.Add("fr-CD", Language.French);                // French (Congo [DRC])
            _dic.Add("fr-CF", Language.French);                // French (Central African Republic)
            _dic.Add("fr-CG", Language.French);                // French (Congo)
            _dic.Add("fr-CH", Language.French);                // French (Switzerland)
            _dic.Add("fr-CI", Language.French);                // French (Côte d’Ivoire)
            _dic.Add("fr-CM", Language.French);                // French (Cameroon)
            _dic.Add("fr-DJ", Language.French);                // French (Djibouti)
            _dic.Add("fr-DZ", Language.French);                // French (Algeria)
            _dic.Add("fr-FR", Language.French);                // French (France)
            _dic.Add("fr-GA", Language.French);                // French (Gabon)
            _dic.Add("fr-GF", Language.French);                // French (French Guiana)
            _dic.Add("fr-GN", Language.French);                // French (Guinea)
            _dic.Add("fr-GP", Language.French);                // French (Guadeloupe)
            _dic.Add("fr-GQ", Language.French);                // French (Equatorial Guinea)
            _dic.Add("fr-HT", Language.French);                // French (Haiti)
            _dic.Add("fr-KM", Language.French);                // French (Comoros)
            _dic.Add("fr-LU", Language.French);                // French (Luxembourg)
            _dic.Add("fr-MA", Language.French);                // French (Morocco)
            _dic.Add("fr-MC", Language.French);                // French (Monaco)
            _dic.Add("fr-MF", Language.French);                // French (St. Martin)
            _dic.Add("fr-MG", Language.French);                // French (Madagascar)
            _dic.Add("fr-ML", Language.French);                // French (Mali)
            _dic.Add("fr-MQ", Language.French);                // French (Martinique)
            _dic.Add("fr-MR", Language.French);                // French (Mauritania)
            _dic.Add("fr-MU", Language.French);                // French (Mauritius)
            _dic.Add("fr-NC", Language.French);                // French (New Caledonia)
            _dic.Add("fr-NE", Language.French);                // French (Niger)
            _dic.Add("fr-PF", Language.French);                // French (French Polynesia)
            _dic.Add("fr-PM", Language.French);                // French (St. Pierre & Miquelon)
            _dic.Add("fr-RE", Language.French);                // French (Réunion)
            _dic.Add("fr-RW", Language.French);                // French (Rwanda)
            _dic.Add("fr-SC", Language.French);                // French (Seychelles)
            _dic.Add("fr-SN", Language.French);                // French (Senegal)
            _dic.Add("fr-SY", Language.French);                // French (Syria)
            _dic.Add("fr-TD", Language.French);                // French (Chad)
            _dic.Add("fr-TG", Language.French);                // French (Togo)
            _dic.Add("fr-TN", Language.French);                // French (Tunisia)
            _dic.Add("fr-VU", Language.French);                // French (Vanuatu)
            _dic.Add("fr-WF", Language.French);                // French (Wallis & Futuna)
            _dic.Add("fr-YT", Language.French);                // French (Mayotte)
            // _dic.Add("fur", Language);                  // Friulian
            // _dic.Add("fur-IT", Language);               // Friulian (Italy)
            // _dic.Add("fy", Language);                   // Western Frisian
            // _dic.Add("fy-NL", Language);                // Western Frisian (Netherlands)
            // _dic.Add("ga", Language);                   // Irish
            // _dic.Add("ga-IE", Language);                // Irish (Ireland)
            // _dic.Add("gd", Language);                   // Scottish Gaelic
            // _dic.Add("gd-GB", Language);                // Scottish Gaelic (United Kingdom)
            // _dic.Add("gl", Language);                   // Galician
            // _dic.Add("gl-ES", Language);                // Galician (Spain)
            // _dic.Add("gn", Language);                   // Guarani
            // _dic.Add("gn-PY", Language);                // Guarani (Paraguay)
            // _dic.Add("gsw", Language);                  // Swiss German
            // _dic.Add("gsw-CH", Language);               // Swiss German (Switzerland)
            // _dic.Add("gsw-FR", Language);               // Swiss German (France)
            // _dic.Add("gsw-LI", Language);               // Swiss German (Liechtenstein)
            // _dic.Add("gu", Language);                   // Gujarati
            // _dic.Add("gu-IN", Language);                // Gujarati (India)
            // _dic.Add("guz", Language);                  // Gusii
            // _dic.Add("guz-KE", Language);               // Gusii (Kenya)
            // _dic.Add("gv", Language);                   // Manx
            // _dic.Add("gv-IM", Language);                // Manx (Isle of Man)
            // _dic.Add("ha", Language);                   // Hausa
            // _dic.Add("ha-GH", Language);                // Hausa (Ghana)
            // _dic.Add("ha-NE", Language);                // Hausa (Niger)
            // _dic.Add("ha-NG", Language);                // Hausa (Nigeria)
            // _dic.Add("haw", Language);                  // Hawaiian
            // _dic.Add("haw-US", Language);               // Hawaiian (United States)
            // _dic.Add("he", Language);                   // Hebrew
            // _dic.Add("he-IL", Language);                // Hebrew (Israel)
            // _dic.Add("hi", Language);                   // Hindi
            // _dic.Add("hi-IN", Language);                // Hindi (India)
            // _dic.Add("hr", Language);                   // Croatian
            // _dic.Add("hr-BA", Language);                // Croatian (Bosnia & Herzegovina)
            // _dic.Add("hr-HR", Language);                // Croatian (Croatia)
            // _dic.Add("hsb", Language);                  // Upper Sorbian
            // _dic.Add("hsb-DE", Language);               // Upper Sorbian (Germany)
            _dic.Add("hu", Language.Hungarian);                   // Hungarian
            _dic.Add("hu-HU", Language.Hungarian);                // Hungarian (Hungary)
            // _dic.Add("hy", Language);                   // Armenian
            // _dic.Add("hy-AM", Language);                // Armenian (Armenia)
            // _dic.Add("ia", Language);                   // Interlingua
            // _dic.Add("ia-001", Language);               // Interlingua (World)
            // _dic.Add("id", Language);                   // Indonesian
            // _dic.Add("id-ID", Language);                // Indonesian (Indonesia)
            // _dic.Add("ig", Language);                   // Igbo
            // _dic.Add("ig-NG", Language);                // Igbo (Nigeria)
            // _dic.Add("ii", Language);                   // Yi
            // _dic.Add("ii-CN", Language);                // Yi (China)
            // _dic.Add("is", Language);                   // Icelandic
            // _dic.Add("is-IS", Language);                // Icelandic (Iceland)
            _dic.Add("it", Language.Italian);                   // Italian
            _dic.Add("it-CH", Language.Italian);                // Italian (Switzerland)
            _dic.Add("it-IT", Language.Italian);                // Italian (Italy)
            _dic.Add("it-SM", Language.Italian);                // Italian (San Marino)
            _dic.Add("it-VA", Language.Italian);                // Italian (Vatican City)
            // _dic.Add("iu", Language);                   // Inuktitut
            // _dic.Add("iu-CA", Language);                // Inuktitut (Canada)
            // _dic.Add("iu-Latn", Language);              // Inuktitut (Latin)
            // _dic.Add("iu-Latn-CA", Language);           // Inuktitut (Latin, Canada)
            _dic.Add("ja", Language.Japanese);                   // Japanese
            _dic.Add("ja-JP", Language.Japanese);                // Japanese (Japan)
            //  _dic.Add("jgo", Language);                  // Ngomba
            //  _dic.Add("jgo-CM", Language);               // Ngomba (Cameroon)
            //  _dic.Add("jmc", Language);                  // Machame
            //  _dic.Add("jmc-TZ", Language);               // Machame (Tanzania)
            //  _dic.Add("jv", Language);                   // Javanese
            //  _dic.Add("jv-ID", Language);                // Javanese (Indonesia)
            //  _dic.Add("ka", Language);                   // Georgian
            //  _dic.Add("ka-GE", Language);                // Georgian (Georgia)
            //  _dic.Add("kab", Language);                  // Kabyle
            //  _dic.Add("kab-DZ", Language);               // Kabyle (Algeria)
            //  _dic.Add("kam", Language);                  // Kamba
            //  _dic.Add("kam-KE", Language);               // Kamba (Kenya)
            //  _dic.Add("kde", Language);                  // Makonde
            //  _dic.Add("kde-TZ", Language);               // Makonde (Tanzania)
            //  _dic.Add("kea", Language);                  // Kabuverdianu
            //  _dic.Add("kea-CV", Language);               // Kabuverdianu (Cabo Verde)
            //  _dic.Add("khq", Language);                  // Koyra Chiini
            //  _dic.Add("khq-ML", Language);               // Koyra Chiini (Mali)
            //  _dic.Add("ki", Language);                   // Kikuyu
            //  _dic.Add("ki-KE", Language);                // Kikuyu (Kenya)
            //  _dic.Add("kk", Language);                   // Kazakh
            //  _dic.Add("kk-KZ", Language);                // Kazakh (Kazakhstan)
            //  _dic.Add("kkj", Language);                  // Kako
            //  _dic.Add("kkj-CM", Language);               // Kako (Cameroon)
            //  _dic.Add("kl", Language);                   // Kalaallisut
            //  _dic.Add("kl-GL", Language);                // Kalaallisut (Greenland)
            //  _dic.Add("kln", Language);                  // Kalenjin
            //  _dic.Add("kln-KE", Language);               // Kalenjin (Kenya)
            //  _dic.Add("km", Language);                   // Khmer
            //  _dic.Add("km-KH", Language);                // Khmer (Cambodia)
            //  _dic.Add("kn", Language);                   // Kannada
            //  _dic.Add("kn-IN", Language);                // Kannada (India)
            //  _dic.Add("ko", Language);                   // Korean
            //  _dic.Add("ko-KP", Language);                // Korean (North Korea)
            //  _dic.Add("ko-KR", Language);                // Korean (Korea)
            //  _dic.Add("kok", Language);                  // Konkani
            //  _dic.Add("kok-IN", Language);               // Konkani (India)
            //  _dic.Add("ks", Language);                   // Kashmiri
            //  _dic.Add("ks-IN", Language);                // Kashmiri (India)
            //  _dic.Add("ksb", Language);                  // Shambala
            //  _dic.Add("ksb-TZ", Language);               // Shambala (Tanzania)
            //  _dic.Add("ksf", Language);                  // Bafia
            //  _dic.Add("ksf-CM", Language);               // Bafia (Cameroon)
            //  _dic.Add("ksh", Language);                  // Colognian
            //  _dic.Add("ksh-DE", Language);               // Colognian (Germany)
            //  _dic.Add("kw", Language);                   // Cornish
            //  _dic.Add("kw-GB", Language);                // Cornish (United Kingdom)
            //  _dic.Add("ky", Language);                   // Kyrgyz
            //  _dic.Add("ky-KG", Language);                // Kyrgyz (Kyrgyzstan)
            //  _dic.Add("lag", Language);                  // Langi
            //  _dic.Add("lag-TZ", Language);               // Langi (Tanzania)
            //  _dic.Add("lb", Language);                   // Luxembourgish
            //  _dic.Add("lb-LU", Language);                // Luxembourgish (Luxembourg)
            //  _dic.Add("lg", Language);                   // Ganda
            //  _dic.Add("lg-UG", Language);                // Ganda (Uganda)
            //  _dic.Add("lkt", Language);                  // Lakota
            //  _dic.Add("lkt-US", Language);               // Lakota (United States)
            //  _dic.Add("ln", Language);                   // Lingala
            //  _dic.Add("ln-AO", Language);                // Lingala (Angola)
            //  _dic.Add("ln-CD", Language);                // Lingala (Congo [DRC])
            //  _dic.Add("ln-CF", Language);                // Lingala (Central African Republic)
            //  _dic.Add("ln-CG", Language);                // Lingala (Congo)
            //  _dic.Add("lo", Language);                   // Lao
            //  _dic.Add("lo-LA", Language);                // Lao (Laos)
            //  _dic.Add("lrc", Language);                  // Northern Luri
            //  _dic.Add("lrc-IQ", Language);               // Northern Luri (Iraq)
            //  _dic.Add("lrc-IR", Language);               // Northern Luri (Iran)
            _dic.Add("lt", Language.Lithuanian);                   // Lithuanian
            _dic.Add("lt-LT", Language.Lithuanian);                // Lithuanian (Lithuania)
            // _dic.Add("lu", Language);                   // Luba-Katanga
            // _dic.Add("lu-CD", Language);                // Luba-Katanga (Congo [DRC])
            // _dic.Add("luo", Language);                  // Luo
            // _dic.Add("luo-KE", Language);               // Luo (Kenya)
            // _dic.Add("luy", Language);                  // Luyia
            // _dic.Add("luy-KE", Language);               // Luyia (Kenya)
            _dic.Add("lv", Language.Latvian);                   // Latvian
            _dic.Add("lv-LV", Language.Latvian);                // Latvian (Latvia)
            //  _dic.Add("mas", Language);                  // Masai
            //  _dic.Add("mas-KE", Language);               // Masai (Kenya)
            //  _dic.Add("mas-TZ", Language);               // Masai (Tanzania)
            //  _dic.Add("mer", Language);                  // Meru
            //  _dic.Add("mer-KE", Language);               // Meru (Kenya)
            //  _dic.Add("mfe", Language);                  // Morisyen
            //  _dic.Add("mfe-MU", Language);               // Morisyen (Mauritius)
            //  _dic.Add("mg", Language);                   // Malagasy
            //  _dic.Add("mg-MG", Language);                // Malagasy (Madagascar)
            //  _dic.Add("mgh", Language);                  // Makhuwa-Meetto
            //  _dic.Add("mgh-MZ", Language);               // Makhuwa-Meetto (Mozambique)
            //  _dic.Add("mgo", Language);                  // Metaʼ
            //  _dic.Add("mgo-CM", Language);               // Metaʼ (Cameroon)
            //  _dic.Add("mi", Language);                   // Maori
            //  _dic.Add("mi-NZ", Language);                // Maori (New Zealand)
            //  _dic.Add("mk", Language);                   // Macedonian
            //  _dic.Add("mk-MK", Language);                // Macedonian (North Macedonia)
            //  _dic.Add("ml", Language);                   // Malayalam
            //  _dic.Add("ml-IN", Language);                // Malayalam (India)
            //  _dic.Add("mn", Language);                   // Mongolian
            //  _dic.Add("mn-MN", Language);                // Mongolian (Mongolia)
            //  _dic.Add("mn-Mong", Language);              // Mongolian (Mongolian)
            //  _dic.Add("mn-Mong-CN", Language);           // Mongolian (Mongolian, China)
            //  _dic.Add("mn-Mong-MN", Language);           // Mongolian (Mongolian, Mongolia)
            //  _dic.Add("moh", Language);                  // Mohawk
            //  _dic.Add("moh-CA", Language);               // Mohawk (Canada)
            //  _dic.Add("mr", Language);                   // Marathi
            //  _dic.Add("mr-IN", Language);                // Marathi (India)
            //  _dic.Add("ms", Language);                   // Malay
            //  _dic.Add("ms-BN", Language);                // Malay (Brunei)
            //  _dic.Add("ms-MY", Language);                // Malay (Malaysia)
            //  _dic.Add("ms-SG", Language);                // Malay (Singapore)
            //  _dic.Add("mt", Language);                   // Maltese
            //  _dic.Add("mt-MT", Language);                // Maltese (Malta)
            //  _dic.Add("mua", Language);                  // Mundang
            //  _dic.Add("mua-CM", Language);               // Mundang (Cameroon)
            //  _dic.Add("my", Language);                   // Burmese
            //  _dic.Add("my-MM", Language);                // Burmese (Myanmar)
            //  _dic.Add("mzn", Language);                  // Mazanderani
            //  _dic.Add("mzn-IR", Language);               // Mazanderani (Iran)
            //  _dic.Add("naq", Language);                  // Nama
            //  _dic.Add("naq-NA", Language);               // Nama (Namibia)
            //  _dic.Add("nb", Language);                   // Norwegian Bokmål
            //  _dic.Add("nb-NO", Language);                // Norwegian Bokmål (Norway)
            //  _dic.Add("nb-SJ", Language);                // Norwegian Bokmål (Svalbard & Jan Mayen)
            //  _dic.Add("nd", Language);                   // North Ndebele
            //  _dic.Add("nd-ZW", Language);                // North Ndebele (Zimbabwe)
            //  _dic.Add("nds", Language);                  // Low German
            //  _dic.Add("nds-DE", Language);               // Low German (Germany)
            //  _dic.Add("nds-NL", Language);               // Low German (Netherlands)
            //  _dic.Add("ne", Language);                   // Nepali
            //  _dic.Add("ne-IN", Language);                // Nepali (India)
            //  _dic.Add("ne-NP", Language);                // Nepali (Nepal)
            _dic.Add("nl", Language.Dutch);                   // Dutch
            _dic.Add("nl-AW", Language.Dutch);                // Dutch (Aruba)
            _dic.Add("nl-BE", Language.Dutch);                // Dutch (Belgium)
            _dic.Add("nl-BQ", Language.Dutch);                // Dutch (Bonaire, Sint Eustatius and Saba)
            _dic.Add("nl-CW", Language.Dutch);                // Dutch (Curaçao)
            _dic.Add("nl-NL", Language.Dutch);                // Dutch (Netherlands)
            _dic.Add("nl-SR", Language.Dutch);                // Dutch (Suriname)
            _dic.Add("nl-SX", Language.Dutch);                // Dutch (Sint Maarten)
            //  _dic.Add("nmg", Language);                  // Kwasio
            //  _dic.Add("nmg-CM", Language);               // Kwasio (Cameroon)
            //  _dic.Add("nn", Language);                   // Norwegian Nynorsk
            //  _dic.Add("nn-NO", Language);                // Norwegian Nynorsk (Norway)
            //  _dic.Add("nnh", Language);                  // Ngiemboon
            //  _dic.Add("nnh-CM", Language);               // Ngiemboon (Cameroon)
            //  _dic.Add("nqo", Language);                  // N’Ko
            //  _dic.Add("nqo-GN", Language);               // N’Ko (Guinea)
            //  _dic.Add("nr", Language);                   // South Ndebele
            //  _dic.Add("nr-ZA", Language);                // South Ndebele (South Africa)
            //  _dic.Add("nso", Language);                  // Sesotho sa Leboa
            //  _dic.Add("nso-ZA", Language);               // Sesotho sa Leboa (South Africa)
            //  _dic.Add("nus", Language);                  // Nuer
            //  _dic.Add("nus-SS", Language);               // Nuer (South Sudan)
            //  _dic.Add("nyn", Language);                  // Nyankole
            //  _dic.Add("nyn-UG", Language);               // Nyankole (Uganda)
            //  _dic.Add("oc", Language);                   // Occitan
            //  _dic.Add("oc-FR", Language);                // Occitan (France)
            //  _dic.Add("om", Language);                   // Oromo
            //  _dic.Add("om-ET", Language);                // Oromo (Ethiopia)
            //  _dic.Add("om-KE", Language);                // Oromo (Kenya)
            //  _dic.Add("or", Language);                   // Odia
            //  _dic.Add("or-IN", Language);                // Odia (India)
            //  _dic.Add("os", Language);                   // Ossetic
            //  _dic.Add("os-GE", Language);                // Ossetic (Georgia)
            //  _dic.Add("os-RU", Language);                // Ossetic (Russia)
            //  _dic.Add("pa", Language);                   // Punjabi
            //  _dic.Add("pa-Arab", Language);              // Punjabi (Arabic)
            //  _dic.Add("pa-Arab-PK", Language);           // Punjabi (Arabic, Pakistan)
            //  _dic.Add("pa-Guru", Language);              // Punjabi (Gurmukhi)
            //  _dic.Add("pa-Guru-IN", Language);           // Punjabi (Gurmukhi, India)
            _dic.Add("pl", Language.Polish);                   // Polish
            _dic.Add("pl-PL", Language.Polish);                // Polish (Poland)
            // _dic.Add("prg", Language);                  // Prussian
            // _dic.Add("prg-001", Language);              // Prussian (World)
            // _dic.Add("ps", Language);                   // Pashto
            // _dic.Add("ps-AF", Language);                // Pashto (Afghanistan)
            // _dic.Add("ps-PK", Language);                // Pashto (Pakistan)
            _dic.Add("pt", Language.Portuguese);                   // Portuguese
            _dic.Add("pt-AO", Language.Portuguese);                // Portuguese (Angola)
            _dic.Add("pt-BR", Language.BrazilianPortuguese);                // Portuguese (Brazil)
            _dic.Add("pt-CH", Language.Portuguese);                // Portuguese (Switzerland)
            _dic.Add("pt-CV", Language.Portuguese);                // Portuguese (Cabo Verde)
            _dic.Add("pt-GQ", Language.Portuguese);                // Portuguese (Equatorial Guinea)
            _dic.Add("pt-GW", Language.Portuguese);                // Portuguese (Guinea-Bissau)
            _dic.Add("pt-LU", Language.Portuguese);                // Portuguese (Luxembourg)
            _dic.Add("pt-MO", Language.Portuguese);                // Portuguese (Macao SAR)
            _dic.Add("pt-MZ", Language.Portuguese);                // Portuguese (Mozambique)
            _dic.Add("pt-PT", Language.Portuguese);                // Portuguese (Portugal)
            _dic.Add("pt-ST", Language.Portuguese);                // Portuguese (São Tomé & Príncipe)
            _dic.Add("pt-TL", Language.Portuguese);                // Portuguese (Timor-Leste)
            // _dic.Add("qu", Language);                   // Quechua
            // _dic.Add("qu-BO", Language);                // Quechua (Bolivia)
            // _dic.Add("qu-EC", Language);                // Quechua (Ecuador)
            // _dic.Add("qu-PE", Language);                // Quechua (Peru)
            // _dic.Add("quc", Language);                  // Kʼicheʼ
            // _dic.Add("quc-GT", Language);               // Kʼicheʼ (Guatemala)
            // _dic.Add("rm", Language);                   // Romansh
            // _dic.Add("rm-CH", Language);                // Romansh (Switzerland)
            // _dic.Add("rn", Language);                   // Rundi
            // _dic.Add("rn-BI", Language);                // Rundi (Burundi)
            _dic.Add("ro", Language.Romanian);                   // Romanian
            _dic.Add("ro-MD", Language.Romanian);                // Romanian (Moldova)
            _dic.Add("ro-RO", Language.Romanian);                // Romanian (Romania)
            // _dic.Add("rof", Language);                  // Rombo
            // _dic.Add("rof-TZ", Language);               // Rombo (Tanzania)
            _dic.Add("ru", Language.Russian);                   // Russian
            _dic.Add("ru-BY", Language.Russian);                // Russian (Belarus)
            _dic.Add("ru-KG", Language.Russian);                // Russian (Kyrgyzstan)
            _dic.Add("ru-KZ", Language.Russian);                // Russian (Kazakhstan)
            _dic.Add("ru-MD", Language.Russian);                // Russian (Moldova)
            _dic.Add("ru-RU", Language.Russian);                // Russian (Russia)
            _dic.Add("ru-UA", Language.Russian);                // Russian (Ukraine)
            //  _dic.Add("rw", Language);                   // Kinyarwanda
            //  _dic.Add("rw-RW", Language);                // Kinyarwanda (Rwanda)
            //  _dic.Add("rwk", Language);                  // Rwa
            //  _dic.Add("rwk-TZ", Language);               // Rwa (Tanzania)
            //  _dic.Add("sa", Language);                   // Sanskrit
            //  _dic.Add("sa-IN", Language);                // Sanskrit (India)
            //  _dic.Add("sah", Language);                  // Sakha
            //  _dic.Add("sah-RU", Language);               // Sakha (Russia)
            //  _dic.Add("saq", Language);                  // Samburu
            //  _dic.Add("saq-KE", Language);               // Samburu (Kenya)
            //  _dic.Add("sbp", Language);                  // Sangu
            //  _dic.Add("sbp-TZ", Language);               // Sangu (Tanzania)
            //  _dic.Add("sd", Language);                   // Sindhi
            //  _dic.Add("sd-PK", Language);                // Sindhi (Pakistan)
            //  _dic.Add("se", Language);                   // Northern Sami
            //  _dic.Add("se-FI", Language);                // Northern Sami (Finland)
            //  _dic.Add("se-NO", Language);                // Northern Sami (Norway)
            //  _dic.Add("se-SE", Language);                // Northern Sami (Sweden)
            //  _dic.Add("seh", Language);                  // Sena
            //  _dic.Add("seh-MZ", Language);               // Sena (Mozambique)
            //  _dic.Add("ses", Language);                  // Koyraboro Senni
            //  _dic.Add("ses-ML", Language);               // Koyraboro Senni (Mali)
            //  _dic.Add("sg", Language);                   // Sango
            //  _dic.Add("sg-CF", Language);                // Sango (Central African Republic)
            //  _dic.Add("shi", Language);                  // Tachelhit
            //  _dic.Add("shi-Latn", Language);             // Tachelhit (Latin)
            //  _dic.Add("shi-Latn-MA", Language);          // Tachelhit (Latin, Morocco)
            //  _dic.Add("shi-Tfng", Language);             // Tachelhit (Tifinagh)
            //  _dic.Add("shi-Tfng-MA", Language);          // Tachelhit (Tifinagh, Morocco)
            //  _dic.Add("si", Language);                   // Sinhala
            //  _dic.Add("si-LK", Language);                // Sinhala (Sri Lanka)
            _dic.Add("sk", Language.Slovak);                   // Slovak
            _dic.Add("sk-SK", Language.Slovak);                // Slovak (Slovakia)
            _dic.Add("sl", Language.Slovenian);                   // Slovenian
            _dic.Add("sl-SI", Language.Slovenian);                // Slovenian (Slovenia)
            //  _dic.Add("sma", Language);                  // Southern Sami
            //  _dic.Add("sma-NO", Language);               // Southern Sami (Norway)
            //  _dic.Add("sma-SE", Language);               // Southern Sami (Sweden)
            //  _dic.Add("smj", Language);                  // Lule Sami
            //  _dic.Add("smj-NO", Language);               // Lule Sami (Norway)
            //  _dic.Add("smj-SE", Language);               // Lule Sami (Sweden)
            //  _dic.Add("smn", Language);                  // Inari Sami
            //  _dic.Add("smn-FI", Language);               // Inari Sami (Finland)
            //  _dic.Add("sms", Language);                  // Skolt Sami
            //  _dic.Add("sms-FI", Language);               // Skolt Sami (Finland)
            //  _dic.Add("sn", Language);                   // Shona
            //  _dic.Add("sn-ZW", Language);                // Shona (Zimbabwe)
            //  _dic.Add("so", Language);                   // Somali
            //  _dic.Add("so-DJ", Language);                // Somali (Djibouti)
            //  _dic.Add("so-ET", Language);                // Somali (Ethiopia)
            //  _dic.Add("so-KE", Language);                // Somali (Kenya)
            //  _dic.Add("so-SO", Language);                // Somali (Somalia)
            //  _dic.Add("sq", Language);                   // Albanian
            //  _dic.Add("sq-AL", Language);                // Albanian (Albania)
            //  _dic.Add("sq-MK", Language);                // Albanian (North Macedonia)
            //  _dic.Add("sq-XK", Language);                // Albanian (Kosovo)
            //  _dic.Add("sr", Language);                   // Serbian
            //  _dic.Add("sr-Cyrl", Language);              // Serbian (Cyrillic)
            //  _dic.Add("sr-Cyrl-BA", Language);           // Serbian (Cyrillic, Bosnia & Herzegovina)
            //  _dic.Add("sr-Cyrl-ME", Language);           // Serbian (Cyrillic, Montenegro)
            //  _dic.Add("sr-Cyrl-RS", Language);           // Serbian (Cyrillic, Serbia)
            //  _dic.Add("sr-Cyrl-XK", Language);           // Serbian (Cyrillic, Kosovo)
            //  _dic.Add("sr-Latn", Language);              // Serbian (Latin)
            //  _dic.Add("sr-Latn-BA", Language);           // Serbian (Latin, Bosnia & Herzegovina)
            //  _dic.Add("sr-Latn-ME", Language);           // Serbian (Latin, Montenegro)
            //  _dic.Add("sr-Latn-RS", Language);           // Serbian (Latin, Serbia)
            //  _dic.Add("sr-Latn-XK", Language);           // Serbian (Latin, Kosovo)
            //  _dic.Add("ss", Language);                   // siSwati
            //  _dic.Add("ss-SZ", Language);                // siSwati (Eswatini)
            //  _dic.Add("ss-ZA", Language);                // siSwati (South Africa)
            //  _dic.Add("ssy", Language);                  // Saho
            //  _dic.Add("ssy-ER", Language);               // Saho (Eritrea)
            //  _dic.Add("st", Language);                   // Sesotho
            //  _dic.Add("st-LS", Language);                // Sesotho (Lesotho)
            //  _dic.Add("st-ZA", Language);                // Sesotho (South Africa)
            _dic.Add("sv", Language.Swedish);                   // Swedish
            _dic.Add("sv-AX", Language.Swedish);                // Swedish (Åland Islands)
            _dic.Add("sv-FI", Language.Swedish);                // Swedish (Finland)
            _dic.Add("sv-SE", Language.Swedish);                // Swedish (Sweden)
            //  _dic.Add("sw", Language);                   // Kiswahili
            //  _dic.Add("sw-CD", Language);                // Kiswahili (Congo [DRC])
            //  _dic.Add("sw-KE", Language);                // Kiswahili (Kenya)
            //  _dic.Add("sw-TZ", Language);                // Kiswahili (Tanzania)
            //  _dic.Add("sw-UG", Language);                // Kiswahili (Uganda)
            //  _dic.Add("syr", Language);                  // Syriac
            //  _dic.Add("syr-SY", Language);               // Syriac (Syria)
            //  _dic.Add("ta", Language);                   // Tamil
            //  _dic.Add("ta-IN", Language);                // Tamil (India)
            //  _dic.Add("ta-LK", Language);                // Tamil (Sri Lanka)
            //  _dic.Add("ta-MY", Language);                // Tamil (Malaysia)
            //  _dic.Add("ta-SG", Language);                // Tamil (Singapore)
            //  _dic.Add("te", Language);                   // Telugu
            //  _dic.Add("te-IN", Language);                // Telugu (India)
            //  _dic.Add("teo", Language);                  // Teso
            //  _dic.Add("teo-KE", Language);               // Teso (Kenya)
            //  _dic.Add("teo-UG", Language);               // Teso (Uganda)
            //  _dic.Add("tg", Language);                   // Tajik
            //  _dic.Add("tg-TJ", Language);                // Tajik (Tajikistan)
            //  _dic.Add("th", Language);                   // Thai
            //  _dic.Add("th-TH", Language);                // Thai (Thailand)
            //  _dic.Add("ti", Language);                   // Tigrinya
            //  _dic.Add("ti-ER", Language);                // Tigrinya (Eritrea)
            //  _dic.Add("ti-ET", Language);                // Tigrinya (Ethiopia)
            //  _dic.Add("tig", Language);                  // Tigre
            //  _dic.Add("tig-ER", Language);               // Tigre (Eritrea)
            //  _dic.Add("tk", Language);                   // Turkmen
            //  _dic.Add("tk-TM", Language);                // Turkmen (Turkmenistan)
            //  _dic.Add("tn", Language);                   // Setswana
            //  _dic.Add("tn-BW", Language);                // Setswana (Botswana)
            //  _dic.Add("tn-ZA", Language);                // Setswana (South Africa)
            //  _dic.Add("to", Language);                   // Tongan
            //  _dic.Add("to-TO", Language);                // Tongan (Tonga)
            //  _dic.Add("tr", Language);                   // Turkish
            //  _dic.Add("tr-CY", Language);                // Turkish (Cyprus)
            //  _dic.Add("tr-TR", Language);                // Turkish (Turkey)
            //  _dic.Add("ts", Language);                   // Xitsonga
            //  _dic.Add("ts-ZA", Language);                // Xitsonga (South Africa)
            //  _dic.Add("tt", Language);                   // Tatar
            //  _dic.Add("tt-RU", Language);                // Tatar (Russia)
            //  _dic.Add("twq", Language);                  // Tasawaq
            //  _dic.Add("twq-NE", Language);               // Tasawaq (Niger)
            //  _dic.Add("tzm", Language);                  // Central Atlas Tamazight
            //  _dic.Add("tzm-MA", Language);               // Central Atlas Tamazight (Morocco)
            //  _dic.Add("ug", Language);                   // Uyghur
            //  _dic.Add("ug-CN", Language);                // Uyghur (China)
            //  _dic.Add("uk", Language);                   // Ukrainian
            //  _dic.Add("uk-UA", Language);                // Ukrainian (Ukraine)
            //  _dic.Add("ur", Language);                   // Urdu
            //  _dic.Add("ur-IN", Language);                // Urdu (India)
            //  _dic.Add("ur-PK", Language);                // Urdu (Pakistan)
            //  _dic.Add("uz", Language);                   // Uzbek
            //  _dic.Add("uz-Arab", Language);              // Uzbek (Arabic)
            //  _dic.Add("uz-Arab-AF", Language);           // Uzbek (Arabic, Afghanistan)
            //  _dic.Add("uz-Cyrl", Language);              // Uzbek (Cyrillic)
            //  _dic.Add("uz-Cyrl-UZ", Language);           // Uzbek (Cyrillic, Uzbekistan)
            //  _dic.Add("uz-Latn", Language);              // Uzbek (Latin)
            //  _dic.Add("uz-Latn-UZ", Language);           // Uzbek (Latin, Uzbekistan)
            //  _dic.Add("vai", Language);                  // Vai
            //  _dic.Add("vai-Latn", Language);             // Vai (Latin)
            //  _dic.Add("vai-Latn-LR", Language);          // Vai (Latin, Liberia)
            //  _dic.Add("vai-Vaii", Language);             // Vai (Vai)
            //  _dic.Add("vai-Vaii-LR", Language);          // Vai (Vai, Liberia)
            //  _dic.Add("ve", Language);                   // Venda
            //  _dic.Add("ve-ZA", Language);                // Venda (South Africa)
            //  _dic.Add("vi", Language);                   // Vietnamese
            //  _dic.Add("vi-VN", Language);                // Vietnamese (Vietnam)
            //  _dic.Add("vo", Language);                   // Volapük
            //  _dic.Add("vo-001", Language);               // Volapük (World)
            //  _dic.Add("vun", Language);                  // Vunjo
            //  _dic.Add("vun-TZ", Language);               // Vunjo (Tanzania)
            //  _dic.Add("wae", Language);                  // Walser
            //  _dic.Add("wae-CH", Language);               // Walser (Switzerland)
            //  _dic.Add("wal", Language);                  // Wolaytta
            //  _dic.Add("wal-ET", Language);               // Wolaytta (Ethiopia)
            //  _dic.Add("wo", Language);                   // Wolof
            //  _dic.Add("wo-SN", Language);                // Wolof (Senegal)
            //  _dic.Add("xh", Language);                   // isiXhosa
            //  _dic.Add("xh-ZA", Language);                // isiXhosa (South Africa)
            //  _dic.Add("xog", Language);                  // Soga
            //  _dic.Add("xog-UG", Language);               // Soga (Uganda)
            //  _dic.Add("yav", Language);                  // Yangben
            //  _dic.Add("yav-CM", Language);               // Yangben (Cameroon)
            //  _dic.Add("yi", Language);                   // Yiddish
            //  _dic.Add("yi-001", Language);               // Yiddish (World)
            //  _dic.Add("yo", Language);                   // Yoruba
            //  _dic.Add("yo-BJ", Language);                // Yoruba (Benin)
            //  _dic.Add("yo-NG", Language);                // Yoruba (Nigeria)
            //  _dic.Add("zgh", Language);                  // Standard Moroccan Tamazight
            //  _dic.Add("zgh-MA", Language);               // Standard Moroccan Tamazight (Morocco)
            _dic.Add("zh", Language.Chinese);                   // Chinese
            _dic.Add("zh-Hans", Language.Chinese);              // Chinese (Simplified)
            _dic.Add("zh-Hans-CN", Language.Chinese);           // Chinese (Simplified, China)
            _dic.Add("zh-Hans-HK", Language.Chinese);           // Chinese (Simplified, Hong Kong SAR)
            _dic.Add("zh-Hans-MO", Language.Chinese);           // Chinese (Simplified, Macao SAR)
            _dic.Add("zh-Hans-SG", Language.Chinese);           // Chinese (Simplified, Singapore)
            _dic.Add("zh-Hant", Language.Chinese);              // Chinese (Traditional)
            _dic.Add("zh-Hant-HK", Language.Chinese);           // Chinese (Traditional, Hong Kong SAR)
            _dic.Add("zh-Hant-MO", Language.Chinese);           // Chinese (Traditional, Macao SAR)
            _dic.Add("zh-Hant-TW", Language.Chinese);           // Chinese (Traditional, Taiwan)
            // _dic.Add("zu", Language);                   // isiZulu
            // _dic.Add("zu-ZA", Language);                // isiZulu (South Africa)


        }


        public TranslateServiceByRemote(ConfigureTranslateServiceByRemote configuration)
        {
            this._configuration = configuration;
            index = 0;
        }

        public string Translate(CultureInfo cultureSource, CultureInfo cultureTarget, string textToTranslate)
        {


            var source = _dic[cultureSource.IetfLanguageTag];
            var target = _dic[cultureTarget.IetfLanguageTag];

            string key = GetKey();

            using (DeepLClient client = new DeepLClient(key, useFreeApi: this._configuration.UseFreeApi))
            {
                try
                {
                    var service = client.TranslateAsync(textToTranslate, source, target);
                    service.Wait();

                    Translation translation = service.Result;
                    //Console.WriteLine(translation.DetectedSourceLanguage);
                    
                    return translation.Text;

                }
                catch (DeepLException exception)
                {
                    Console.WriteLine($"An error occurred: {exception.Message}");
                }
            }

            return "";
        }

        private string GetKey()
        {

            var result = this._configuration.SecurityKey[index++];

            if (index == this._configuration.SecurityKey.Count)
                index = 0;

            return result.Value;

        }

        private readonly HttpClient _httpClient;
        private readonly ConfigureTranslateServiceByRemote _configuration;
        private int index;
        private readonly static Dictionary<string, Language> _dic = new Dictionary<string, Language>();

    }

}
