namespace SonosWp.Test
{
    public class Xmls
    {
        public class Account
        {
            public const string Xml =
                "<?xml version=\"1.0\" ?>\n<?xml-stylesheet type=\"text/xsl\" href=\"/xml/review.xsl\"?>" +
                "<ZPSupportInfo type=\"User\">" +
                "<Accounts LastUpdateDevice=\"RINCON_000E5874AA8601400\" Version=\"46\" NextSerialNum=\"3\">" +
                "<Account Type=\"5127\" SerialNum=\"1\">" +
                "<UN>25303079</UN>" +
                "<MD>1</MD>" +
                "<NN></NN>" +
                "<OADevID></OADevID>" +
                "<Key></Key>" +
                "</Account>" +
                "<Account Type=\"519\" SerialNum=\"2\">" +
                "<UN>jonas@gottschau.dk</UN>" +
                "<MD>1</MD>" +
                "<NN>jonas</NN>" +
                "<OADevID></OADevID>" +
                "<Key></Key>" +
                "</Account></Accounts>" +
                "</ZPSupportInfo>";
        }

        public class Topology
        {
            public const string Xml =
                "<?xml version=\"1.0\" ?>" +
                "<?xml-stylesheet type=\"text/xsl\" href=\"/xml/review.xsl\"?>" +
                "<ZPSupportInfo type=\"User\">" +
                "<ZonePlayers>" +
                "<ZonePlayer group='RINCON_000E58F3F87C01400:10' coordinator='true' wirelessmode='0' channelfreq='2412' behindwifiext='0' wifienabled='1' location='http://192.168.1.8:1400/xml/device_description.xml' version='28.1-83040' mincompatibleversion='27.0-00000' legacycompatibleversion='24.0-0000' bootseq='25' uuid='RINCON_000E58F3F87C01400'>Kitchen</ZonePlayer>" +
                "<ZonePlayer group='RINCON_000E58F3F87C01400:10' coordinator='false' wirelessmode='0' channelfreq='2412' behindwifiext='0' wifienabled='1' location='http://192.168.1.6:1400/xml/device_description.xml' version='28.1-83040' mincompatibleversion='27.0-00000' legacycompatibleversion='24.0-0000' bootseq='42' uuid='RINCON_000E5874AA8601400'>Living Room</ZonePlayer>" +
                "</ZonePlayers>" +
                "<MediaServers>" +
                "<MediaServer location='10.0.0.100:3401' uuid='mobile-iPhone-EC852F8E0568' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Enghoff</MediaServer>" +
                "<MediaServer location='10.0.0.134:3401' uuid='mobile-iPad-64200C69FC00' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Jonas Gottschau’s iPad</MediaServer>" +
                "<MediaServer location='10.0.0.121:3401' uuid='mobile-iPad-64200C9EE14B' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Maria’s iPad</MediaServer><MediaServer location='10.0.0.119:3401' uuid='mobile-iPhone-3CD0F81C7A73' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Marias iPhone</MediaServer>" +
                "<MediaServer location='10.0.0.134:3401' uuid='mobile-iPad-B7C8160B-CBA2-43C9-B01D-C34D6469EB15' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Jonas Gottschau’s iPad</MediaServer>" +
                "<MediaServer location='10.0.0.145:3500' uuid='mobile-ACR-a80600674dbb' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Samsung GT-I8190</MediaServer>" +
                "<MediaServer location='10.0.0.141:3500' uuid='mobile-ACR-e063e59969af' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Sony C6903</MediaServer>" +
                "<MediaServer location='10.0.0.121:3401' uuid='mobile-iPad-3A7440D6-0DA4-44A0-9565-534989C61478' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Maria’s iPad</MediaServer>" +
                "<MediaServer location='192.168.1.10:3401' uuid='mobile-iPhone-F9407437-032B-4D05-AE3C-C6614D51E472' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Enghoff</MediaServer>" +
                "<MediaServer location='192.168.1.5:3401' uuid='mobile-iPhone-4E765380-A8B5-412B-8E3A-F52F4CDD7F2D' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Enghoff</MediaServer>" +
                "<MediaServer location='192.168.1.7:3500' uuid='mobile-ACR-78a873f8828f' version='' canbedisplayed='false' unavailable='false' type='0' ext=''>Samsung GT-I9195</MediaServer>" +
                "</MediaServers>" +
                "</ZPSupportInfo>";
        }
    }
}
