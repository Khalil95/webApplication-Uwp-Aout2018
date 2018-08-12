using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NewsFeed
    {
        public IEnumerable<Publication> PublicationNewsFeed { get; set; }

        public NewsFeed()
        {
        }
    }
}
