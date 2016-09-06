using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Sonos.Extensions;
using Sonos.Models;

namespace SonosWp.Test
{
    [TestClass]
    public class XmlParseTests
    {
        [TestMethod]
        public void Accounts_ParseTypeAndUsername_IsValid()
        {
            // ARRANGE
            var accountsXElement = XElement.Parse(Xmls.Account.Xml);
            var musicServiceAccounts = new List<MusicServiceAccount>();

            // ACT
            var accounts = accountsXElement.Descendants(XName.Get("Account"));
            foreach (var accountElement in accounts)
            {
                var id = accountElement.TryGetAttributeValue("Type");
                var username = accountElement.TryGetElementValue("UN");
                musicServiceAccounts.Add(new MusicServiceAccount(id, username));
            }

            // ASSERT
            Assert.AreEqual(2, musicServiceAccounts.Count);
            Assert.AreEqual("25303079", musicServiceAccounts.First().UserName);

            Assert.AreEqual("5127", musicServiceAccounts.First().ServiceId);
            Assert.AreEqual("jonas@gottschau.dk", musicServiceAccounts.Last().UserName);
            Assert.AreEqual("519", musicServiceAccounts.Last().ServiceId);
        }
    }
}
