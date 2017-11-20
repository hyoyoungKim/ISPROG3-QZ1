using QZ1.App_Code;
using QZ1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QZ1.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            List<BooksModel> list = new List<BooksModel>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"Select t.titleID, t.titleName, t.titlePrice, t.titlePubDate, t.titleNotes, a.authorLN, a.authorFN, p.pubName
From titles t Inner Join publishers p On t.pubID = p.pubID Inner Join authors a On t.authorID = a.authorID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            list.Add(new BooksModel
                            {
                                ID = Convert.ToInt32(data["titleID"].ToString()),
                                TitleName = data["titleName"].ToString(),
                                Price = data["titlePrice"].ToString(),
                                Notes = data["titleNotes"].ToString(),
                                PublicationDate = DateTime.Parse(data["titlePubDate"].ToString()),
                                LN = data["authorLN"].ToString(),
                                FN = data["authorFN"].ToString(),
                                PN = data["pubName"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }
        public List<SelectListItem> GetP()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"Select pubID, pubName From publishers Order By pubName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = data["pubName"].ToString(),
                                Value = data["pubID"].ToString()
                            });
                        }
                    }
                }
            }
            return items;
        }

        public List<SelectListItem> GetA()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"Select authorID, authorLN, authorFN From authors Order By authorLN";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = data["authorLN"].ToString() +", " + data["authorFN"].ToString(),
                                Value = data["authorID"].ToString()
                            });
                        }
                    }
                }
            }
            return items;
        }

        public ActionResult Add()
        {
            BooksModel book = new BooksModel();
            book.Publishers = GetP();
            book.Authors = GetA();
            return View(book);
        }

        [HttpPost]
        public ActionResult Add(BooksModel book)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"Insert Into titles Values (@pubID,@authorID,@titleName,@titlePrice,@titlePubDate,@titleNotes)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pubID", book.PubID);
                    cmd.Parameters.AddWithValue("@authorID", book.AuthorID);
                    cmd.Parameters.AddWithValue("@titleName", book.TitleName);
                    cmd.Parameters.AddWithValue("@titlePrice", book.Price);
                    cmd.Parameters.AddWithValue("@titlePubDate", "");
                    cmd.Parameters.AddWithValue("@titleNotes", book.Notes);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}