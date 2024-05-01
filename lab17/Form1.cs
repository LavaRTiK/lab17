using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab17
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                listView1.Items.Clear();
                //MessageBox.Show("Null tx1 and tx2");
                using HttpResponseMessage response =
                await client.GetAsync(@$"https://openlibrary.org/search.json?author={textBox2.Text.ToString().Replace(' ','+')}&title={textBox1.Text.ToString().Replace(' ','+')}&fields=title,author_name,first_publish_year");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<ListBooks>();
                if (content.num_found == 0)
                {
                    listView1.Items.Clear();
                    MessageBox.Show("Not found");
                }
                foreach (var item in content.docs)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { item.author_name[0], item.first_publish_year.ToString(), item.title.ToString() });
                    listView1.Items.Add(listViewItem);
                }
                //MessageBox.Show(content.docs.Count.ToString());
                return;

            }
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                listView1.Items.Clear();
                //MessageBox.Show("Null tx1");
                using HttpResponseMessage response =
                await client.GetAsync(@$"https://openlibrary.org/search.json?title={textBox1.Text.ToString().Replace(' ','+')}&fields=title,author_name,first_publish_year");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<ListBooks>();
                if (content.num_found == 0)
                {
                    listView1.Items.Clear();
                    MessageBox.Show("Not found");
                    return;
                }
                foreach (var item in content.docs)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { item.author_name[0], item.first_publish_year.ToString(), item.title.ToString() });
                    listView1.Items.Add(listViewItem);
                }
                //MessageBox.Show(content.docs.Count.ToString());
                return;
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                listView1.Items.Clear();
                using HttpResponseMessage response =
                await client.GetAsync(@$"https://openlibrary.org/search.json?author={textBox2.Text.ToString().Replace(' ','+')}&fields=title,author_name,first_publish_year");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<ListBooks>();
                if (content.num_found == 0)
                {
                    listView1.Items.Clear();
                    MessageBox.Show("Not found");
                    return;
                }
                foreach (var item in content.docs)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { item.author_name[0], item.first_publish_year.ToString(),item.title.ToString()});
                    listView1.Items.Add(listViewItem);
                }
                //MessageBox.Show(content.docs.Count.ToString());
                return;
            }

        }
    }
}
