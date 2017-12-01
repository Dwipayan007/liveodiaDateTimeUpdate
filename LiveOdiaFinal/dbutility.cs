using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using LiveOdiaFinal.Models;
using System.Globalization;

namespace LiveOdiaFinal {
    public class dbutility {
        public static DataTable getAllTopStory() {
            //string tdate = DateTime.Now.ToString("dd-MM-yyyy");
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM topnews";

                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static bool ReorderImpNews(List<ImpNews> impnews) {

            bool res = false;
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            try {
                foreach (var entry in impnews) {
                    string priority = entry.priority;
                    string inid = entry.inid;
                    scmd.CommandText = "update impnews set priority='" + priority + "' where inid='" + inid + "'";
                    scmd.Prepare();
                    res = Convert.ToBoolean(scmd.ExecuteNonQuery());
                }
                res = true;
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static DataTable getFullRnews(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM fullnews WHERE fnid='" + id + "'";
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static DataTable getRelatedNewsById(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM fullnews WHERE rid='" + id + "'";
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static bool addRelated(Related rdt) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            bool res = false;
            try {
                scon.Open();
                scmd.CommandText = "INSERT INTO relatednews (rnews) VALUES (@rnews)";
                scmd.Parameters.AddWithValue("rnews", rdt.rdata);
                scmd.Connection = scon;
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;
            } catch (Exception ex) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool saveNewsData(Dictionary<string, string> valDict) {
            bool res = false;
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            try {
                foreach (KeyValuePair<string, string> kvp in valDict) {

                    if (kvp.Key == "Topnews") {

                        if (valDict.ContainsKey("tsub")) {
                            scmd.CommandText = "INSERT INTO fullnews (title,sub,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@sub,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("sub", valDict["tsub"]);
                        } else
                            scmd.CommandText = "INSERT INTO fullnews (title,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                        scmd.Parameters.AddWithValue("title", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "Topnews");
                        scmd.Parameters.AddWithValue("fullnews", valDict["tnews"]);
                        scmd.Parameters.AddWithValue("image", valDict["img"]);
                        scmd.Parameters.AddWithValue("newsdate", valDict["todaydate"]);
                        scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                    }
                    if (kvp.Key == "ImpNews") {
                        if (valDict.ContainsKey("isub")) {
                            scmd.CommandText = "INSERT INTO fullnews (title,sub,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@sub,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("sub", valDict["isub"]);
                        } else
                            scmd.CommandText = "INSERT INTO fullnews (title,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                        scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        scmd.Parameters.AddWithValue("title", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "ImpNews");
                        scmd.Parameters.AddWithValue("fullnews", valDict["inews"]);
                        scmd.Parameters.AddWithValue("image", valDict["img"]);
                        scmd.Parameters.AddWithValue("newsdate", valDict["todaydate"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                    }
                    if (kvp.Key == "hotNews") {
                        if (valDict.ContainsKey("hsub")) {
                            scmd.CommandText = "INSERT INTO fullnews (title,sub,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@sub,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("sub", valDict["hsub"]);
                        } else
                            scmd.CommandText = "INSERT INTO fullnews (title,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                        scmd.Parameters.AddWithValue("title", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "hotNews");
                        scmd.Parameters.AddWithValue("fullnews", valDict["hfullNews"]);
                        scmd.Parameters.AddWithValue("image", valDict["img"]);
                        scmd.Parameters.AddWithValue("newsdate", valDict["todaydate"]);
                        scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                    }
                    if (kvp.Key == "Newstory") {
                        if (valDict.ContainsKey("nsub")) {
                            scmd.CommandText = "INSERT INTO fullnews (title,sub,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@sub,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("sub", valDict["nsub"]);
                        } else
                            scmd.CommandText = "INSERT INTO fullnews (title,fullnews,image,newstype,newsdate,mycolor,rid) VALUES(@title,@fullnews,@image,@newstype,@newsdate,@mycolor,@rid)";
                        scmd.Parameters.AddWithValue("title", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "Newstory");
                        scmd.Parameters.AddWithValue("fullnews", valDict["nstory"]);
                        scmd.Parameters.AddWithValue("image", valDict["img"]);
                        scmd.Parameters.AddWithValue("newsdate", valDict["todaydate"]);
                        scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                    }
                }
                res = true;
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static DataTable getRelated() {
            //string tdate = DateTime.Now.ToString("dd-MM-yyyy");
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM relatednews ORDER BY rnews ASC";

                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static bool deleteImpNews(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            bool res = false;
            try {
                scmd.CommandText = "delete from impnews where inid=" + id;
                scmd.Parameters.AddWithValue("inid", id);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static void updateNewsData(Dictionary<string, string> valDict) {
            bool res = false;
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            Dictionary<string, List<HotNewsModel>> fullNews = new Dictionary<string, List<HotNewsModel>>();
            List<HotNewsModel> _newsData = new List<HotNewsModel>();
            try {
                foreach (KeyValuePair<string, string> kvp in valDict) {
                    DateTime dt = Convert.ToDateTime(valDict["updatedon"]);
                    if (kvp.Key == "ImpNews") {
                        if (valDict.ContainsKey("isub")) {
                            scmd.CommandText = "UPDATE impnews SET ititle =@ititle,isub = @isub,impnews = @impnews,iimage = @iimage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@myColor WHERE inid=@inid";
                            scmd.Parameters.AddWithValue("isub", valDict["isub"]);
                        } else
                            scmd.CommandText = "UPDATE impnews SET ititle =@ititle,impnews = @impnews,iimage = @iimage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@myColor WHERE inid=@inid";
                        scmd.Parameters.AddWithValue("inid", valDict["inid"]);
                        scmd.Parameters.AddWithValue("ititle", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "Impnews");

                        scmd.Parameters.AddWithValue("impnews", valDict["inews"]);
                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("iimage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("iimage", "No Image");
                        scmd.Parameters.AddWithValue("updatedon", dt);
                        scmd.Parameters.AddWithValue("priority", valDict["priority"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "UPDATE newsimages SET imgurl=@imgurl,updatedon=@updatedon WHERE inid=@inid";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("inid", valDict["inid"]);
                            scmd.Parameters.AddWithValue("updatedon", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                    }

                    if (kvp.Key == "Topnews") {
                        if (valDict.ContainsKey("tsub")) {
                            scmd.CommandText = "UPDATE topnews SET ttitle =@ttitle,tsub = @tsub,topnews = @topnews,timage = @timage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@mycolor WHERE tnid=@tnid";
                            scmd.Parameters.AddWithValue("tsub", valDict["tsub"]);
                        } else
                            scmd.CommandText = "UPDATE topnews SET ttitle =@ttitle,topnews = @topnews,timage = @timage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@mycolor WHERE tnid=@tnid";
                        scmd.Parameters.AddWithValue("tnid", valDict["tnid"]);
                        scmd.Parameters.AddWithValue("ttitle", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "Topnews");

                        scmd.Parameters.AddWithValue("topnews", valDict["tnews"]);
                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("timage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("timage", "No Image");
                        scmd.Parameters.AddWithValue("updatedon", dt);
                        scmd.Parameters.AddWithValue("priority", valDict["priority"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "UPDATE newsimages SET imgurl=@imgurl,updatedon=@updatedon WHERE tnid=@tnid";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("tnid", valDict["tnid"]);
                            scmd.Parameters.AddWithValue("updatedon", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                    }
                    if (kvp.Key == "hotNews") {
                        if (valDict.ContainsKey("hsub")) {
                            scmd.CommandText = "UPDATE hotnews SET htitle =@htitle,hsub = @hsub,hotnews = @hotnews,himage = @himage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@myColor WHERE hnid=@hnid";
                            scmd.Parameters.AddWithValue("hsub", valDict["hsub"]);
                        } else
                            scmd.CommandText = "UPDATE hotnews SET htitle =@htitle,hotnews = @hotnews,himage = @himage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@myColor WHERE hnid=@hnid";

                        scmd.Parameters.AddWithValue("htitle", valDict["title"]);
                        scmd.Parameters.AddWithValue("hotnews", valDict["hnews"]);
                        //scmd.Parameters.AddWithValue("ndid", valDict["selOption"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "hotNews");

                        scmd.Parameters.AddWithValue("hnid", valDict["hnid"]);

                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("himage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("himage", "No Image");
                        scmd.Parameters.AddWithValue("updatedon", dt);
                        scmd.Parameters.AddWithValue("priority", valDict["priority"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "UPDATE newsimages SET imgurl=@imgurl,updatedon=@updatedon WHERE hnid=@hnid";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("hnid", valDict["hnid"]);
                            scmd.Parameters.AddWithValue("updatedon", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                    }
                    if (kvp.Key == "Newstory") {

                        //DateTime dt = DateTime.ParseExact(valDict["updatedon"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (valDict.ContainsKey("nsub")) {
                            scmd.CommandText = "UPDATE newstory SET ntitle =@ntitle,nsub = @nsub,newstory = @newstory,nimage = @nimage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@myColor WHERE nsid=@nsid";
                            scmd.Parameters.AddWithValue("nsub", valDict["nsub"]);
                        } else
                            scmd.CommandText = "UPDATE newstory SET ntitle =@ntitle,newstory = @newstory,nimage = @nimage,newstype =@newstype,updatedon=@updatedon,priority=@priority,mycolor=@myColor WHERE nsid=@nsid";
                        scmd.Parameters.AddWithValue("ntitle", valDict["title"]);
                        scmd.Parameters.AddWithValue("newstory", valDict["nstory"]);
                        scmd.Parameters.AddWithValue("nsid", valDict["nsid"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "hotNews");

                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("nimage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("nimage", "No Image");
                        scmd.Parameters.AddWithValue("updatedon", dt);
                        scmd.Parameters.AddWithValue("priority", valDict["priority"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "UPDATE newsimages SET imgurl=@imgurl,updatedon=@updatedon WHERE nsid=@nsid";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("nsid", valDict["nsid"]);
                            scmd.Parameters.AddWithValue("updatedon", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                    }

                }
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }

        }

        public static DataTable getPriorityNews() {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT inid,ititle,priority  FROM impnews WHERE  NOT priority='null' AND NOT priority='7'";

                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static void saveuserVisit(object v) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "Select usercount from uservisit";
                scmd.Prepare();
                int lastuservisitcount = Convert.ToInt32(scmd.ExecuteScalar());
                scmd.Parameters.Clear();
                if (lastuservisitcount > 0) {
                    ++lastuservisitcount;
                    scmd.CommandText = "update uservisit set usercount=" + lastuservisitcount;
                } else {
                    scmd.CommandText = "insert into uservisit(usercount) values (@usercount)";
                    scmd.Parameters.AddWithValue("usercount", ++lastuservisitcount);
                }
                scmd.Prepare();
                scmd.ExecuteNonQuery();
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
        }

        public static DataTable getImpNewsById(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM impnews where inid='" + id + "'";
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static DataTable GetImpNews() {
            // string tdate = DateTime.Now.ToString("dd-MM-yyyy");
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM impnews";
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static DataTable getAllNews(AdminModel val) {
            //string tdate = val.udatedon;
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM topnews";
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static bool createPdfName(string pdfname, AdminModel val) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            string pdfdate = val.updatedon;
            bool res = false;
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "INSERT INTO newspdf (pdfname, pdfdate) VALUES (@pdfname,@pdfdate)";
                scmd.Parameters.AddWithValue("pdfname", pdfname);
                scmd.Parameters.AddWithValue("pdfdate", pdfdate);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;
            } catch (Exception ex) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool updateudatedon(AdminModel udatedon) {
            bool res = false;
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            Dictionary<string, List<HotNewsModel>> fullNews = new Dictionary<string, List<HotNewsModel>>();
            List<HotNewsModel> _newsData = new List<HotNewsModel>();
            try {
                string newsdt = udatedon.updatedon;
                scmd.CommandText = "Update newstory set udatedon='" + newsdt + "'";
                //scmd.Parameters.AddWithValue("udatedon", udatedon.udatedon);
                scmd.Prepare();
                res = Convert.ToBoolean(scmd.ExecuteNonQuery());
                if (res) {
                    scmd.Parameters.Clear();
                    scmd.CommandText = "Update topnews set udatedon='" + newsdt + "'";
                    //scmd.Parameters.AddWithValue("udatedon", udatedon.udatedon);
                    scmd.Prepare();
                    res = Convert.ToBoolean(scmd.ExecuteNonQuery());
                    if (res) {
                        scmd.Parameters.Clear();
                        scmd.CommandText = "Update hotnews set udatedon='" + newsdt + "'";
                        // scmd.Parameters.AddWithValue("udatedon", udatedon.udatedon);
                        scmd.Prepare();
                        res = Convert.ToBoolean(scmd.ExecuteNonQuery());
                    }
                    if (res) {
                        scmd.Parameters.Clear();
                        scmd.CommandText = "Update impnews set udatedon='" + newsdt + "'";
                        // scmd.Parameters.AddWithValue("udatedon", udatedon.udatedon);
                        scmd.Prepare();
                        res = Convert.ToBoolean(scmd.ExecuteNonQuery());
                    }
                }
                res = true;
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool getLoginData(Login ldata) {
            bool res = false;
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            Dictionary<string, List<HotNewsModel>> fullNews = new Dictionary<string, List<HotNewsModel>>();
            List<HotNewsModel> _newsData = new List<HotNewsModel>();
            try {
                scmd.CommandText = "SELECT * FROM login WHERE uname='" + ldata.USERNAME + "' AND pword='" + ldata.PASSWORD + "'";
                scmd.Prepare();
                res = Convert.ToBoolean(scmd.ExecuteScalar());

            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static DataTable GetHotNewsData() {
            //string tdate = DateTime.Now.ToString("dd-MM-yyyy");
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM hotnews";

                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static bool DeleteAllNews(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            bool res = false;
            int result = 0;
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "TRUNCATE TABLE hotnews";
                scmd.Prepare();
                result = scmd.ExecuteNonQuery();
                if (result == 0) {
                    scmd.Parameters.Clear();
                    scmd.CommandText = "TRUNCATE TABLE newstory";
                    scmd.Prepare();
                    result = scmd.ExecuteNonQuery();
                }
                if (result == 0) {
                    scmd.Parameters.Clear();
                    scmd.CommandText = "TRUNCATE TABLE topnews";
                    scmd.Prepare();
                    result = scmd.ExecuteNonQuery();
                }
                res = true;
            } catch (Exception ex) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static DataTable getHotNewsSummaryDetail(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM hotnews where hnid=" + id;
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static bool deleteHotNews(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            bool res = false;
            try {
                scmd.CommandText = "delete from hotnews where hnid=" + id;
                scmd.Parameters.AddWithValue("hnid", id);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool deleteNewsStory(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            bool res = false;
            try {
                scmd.CommandText = "delete from newstory where nsid=" + id;
                scmd.Parameters.AddWithValue("nsid", id);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool deleteTopNews(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            bool res = false;
            try {
                scmd.CommandText = "delete from topnews where tnid=" + id;
                scmd.Parameters.AddWithValue("tnid", id);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static DataTable getNewstoryById(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM newstory where nsid=" + id;
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static DataTable getAllNewStory() {
            // string tdate = DateTime.Now.ToString("dd-MM-yyyy");
            //string yesterday = DateTime.Today.AddDays(-2).ToString("dd-MM-yyyy");
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM newstory";

                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static DataTable getHotNewsSummary(int id) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM hotnews WHERE ndid=" + id;
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static DataTable GetHotNewsTitle() {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM newsdivision";
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static DataTable getTopNewsById(int id) {

            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            DataTable dt = new DataTable();
            try {
                scon.Open();
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM topnews where tnid=" + id;
                scmd.Prepare();
                dt.Load(scmd.ExecuteReader());
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return dt;
        }

        public static bool addNewCategory(AdminModel value) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            bool res = false;
            try {
                scon.Open();
                scmd.CommandText = "INSERT INTO newsdivision (newsdiv) VALUES (@newsdiv)";
                scmd.Parameters.AddWithValue("newsdiv", value.CNAME);
                scmd.Connection = scon;
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                res = true;
            } catch (Exception ex) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        public static bool SaveData(Dictionary<string, string> valDict) {
            bool res = false;
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            try {
                foreach (KeyValuePair<string, string> kvp in valDict) {
                    DateTime dt = Convert.ToDateTime(valDict["updatedon"]);
                    if (kvp.Key == "Topnews") {
                        if (valDict.ContainsKey("tsub") && valDict.ContainsKey("relateNews")) {

                            scmd.CommandText = "INSERT INTO topnews (ttitle,tsub,topnews,timage,newstype,newsdate,updatedon,mycolor,rid) VALUES(@ttitle,@tsub,@topnews,@timage,@newstype,@updatedon,@updatedon,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("tsub", valDict["tsub"]);
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("relateNews") && !valDict.ContainsKey("tsub")) {
                            scmd.CommandText = "INSERT INTO topnews (ttitle,topnews,timage,newstype,newsdate,updatedon,mycolor,rid) VALUES(@ttitle,@topnews,@timage,@newstype,@updatedon,@updatedon,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("tsub") && !valDict.ContainsKey("relateNews")) {
                            scmd.CommandText = "INSERT INTO topnews (ttitle,tsub,topnews,timage,newstype,newsdate,updatedon,mycolor,rid) VALUES(@ttitle,@tsub,@topnews,@timage,@newstype,@updatedon,@updatedon,@mycolor)";
                            scmd.Parameters.AddWithValue("tsub", valDict["tsub"]);
                        } else
                            scmd.CommandText = "INSERT INTO topnews (ttitle,topnews,timage,newstype,newsdate,updatedon,mycolor) VALUES(@ttitle,@topnews,@timage,@newstype,@updatedon,@updatedon,@mycolor)";

                        scmd.Parameters.AddWithValue("ttitle", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "Topnews");

                        scmd.Parameters.AddWithValue("topnews", valDict["tnews"]);
                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("timage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("timage", "No Image");
                        scmd.Parameters.AddWithValue("updatedon", dt);
                        scmd.Parameters.AddWithValue("newsdate", dt);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "INSERT INTO newsimages(imgurl,tnid,updatedon,newsdate) values (@imgurl,@tnid,@updatedon,@updatedon)";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("tnid", lid);
                            scmd.Parameters.AddWithValue("updatedon", dt);
                            scmd.Parameters.AddWithValue("newsdate", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                        res = true;
                    }

                    if (kvp.Key == "ImpNews") {
                        var pr = valDict["priority"].ToString();
                        if (pr != "undefined") {
                            updatePriority(valDict);
                        }

                        if (valDict.ContainsKey("isub") && valDict.ContainsKey("relateNews")) {
                            scmd.CommandText = "INSERT INTO impnews (ititle,isub,impnews,iimage,newstype,newsdate,updatedon,priority,mycolor,rid) VALUES(@ititle,@isub,@impnews,@iimage,@newstype,@updatedon,@updatedon,@priority,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("isub", valDict["isub"]);
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("relateNews") && !valDict.ContainsKey("isub")) {
                            scmd.CommandText = "INSERT INTO impnews (ititle,impnews,iimage,newstype,newsdate,updatedon,priority,mycolor,rid) VALUES(@ititle,@impnews,@iimage,@newstype,@updatedon,@updatedon,@priority,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("isub") && !valDict.ContainsKey("relateNews")) {
                            scmd.CommandText = "INSERT INTO impnews (ititle,isub,impnews,iimage,newstype,newsdate,udatedon,priority,mycolor) VALUES(@ititle,@isub,@impnews,@iimage,@newstype,@updatedon,@updatedon,@priority,@mycolor)";
                            scmd.Parameters.AddWithValue("isub", valDict["isub"]);
                        } else
                            scmd.CommandText = "INSERT INTO impnews (ititle,impnews,iimage,newstype,newsdate,updatedon,priority,mycolor) VALUES(@ititle,@impnews,@iimage,@newstype,@updatedon,@updatedon,@priority,@mycolor)";
                        //scmd.Parameters.AddWithValue("lid", valDict["HotNews"]);
                        scmd.Parameters.AddWithValue("ititle", valDict["title"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "Impnews");

                        scmd.Parameters.AddWithValue("impnews", valDict["inews"]);
                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("iimage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("iimage", "No Image");
                        scmd.Parameters.AddWithValue("newsdate", dt);
                        scmd.Parameters.AddWithValue("updatedon", dt);
                        scmd.Parameters.AddWithValue("priority", valDict["priority"]);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "INSERT INTO newsimages(imgurl,inid,udatedon,newsdate) values (@imgurl,@inid,@updatedon,@updatedon)";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("inid", lid);
                            scmd.Parameters.AddWithValue("updatedon", dt);
                            scmd.Parameters.AddWithValue("newsdate", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                        res = true;
                    }

                    if (kvp.Key == "hotNews") {
                        if (valDict.ContainsKey("hsub") && valDict.ContainsKey("relateNews")) {
                            scmd.CommandText = "INSERT INTO hotnews (htitle,hsub,hotnews,himage,newstype,newsdate,updatedon,ndid,mycolor,rid) VALUES(@htitle,@hsub,@hotnews,@himage,@newstype,@updatedon,@updatedon,@ndid,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("hsub", valDict["hsub"]);
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("relateNews") && !valDict.ContainsKey("hsub")) {
                            scmd.CommandText = "INSERT INTO hotnews (htitle,hotnews,himage,newstype,newsdate,updatedon,ndid,mycolor,rid) VALUES(@htitle,@hotnews,@himage,@newstype,@updatedon,@updatedon,@ndid,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("hsub") && !valDict.ContainsKey("relateNews")) {
                            scmd.CommandText = "INSERT INTO hotnews (htitle,hsub,hotnews,himage,newstype,updatedon,ndid,mycolor) VALUES(@htitle,@hsub,@hotnews,@himage,@newstype,@updatedon,@updatedon,@ndid,@mycolor)";
                            scmd.Parameters.AddWithValue("hsub", valDict["hsub"]);
                        } else {
                            scmd.CommandText = "INSERT INTO hotnews (htitle,hotnews,himage,newstype,newsdate,updatedon,ndid,mycolor) VALUES(@htitle,@hotnews,@himage,@newstype,@updatedon,@updatedon,@ndid,@mycolor)";
                        }
                        scmd.Parameters.AddWithValue("htitle", valDict["title"]);
                        scmd.Parameters.AddWithValue("hotnews", valDict["hfullNews"]);
                        scmd.Parameters.AddWithValue("ndid", valDict["selOption"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "hotNews");
                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("himage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("himage", "No Image");
                        scmd.Parameters.AddWithValue("updatedon", dt);
                        scmd.Parameters.AddWithValue("newsdate", dt);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "INSERT INTO newsimages(imgurl,hnid,udatedon,newsdate) values (@imgurl,@hnid,@updatedon,@updatedon)";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("hnid", lid);
                            scmd.Parameters.AddWithValue("udatedon", dt);
                            scmd.Parameters.AddWithValue("newsdate", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                        res = true;
                    }
                    if (kvp.Key == "Newstory") {
                        if (valDict.ContainsKey("nsub") && valDict.ContainsKey("relateNews")) {
                            scmd.CommandText = "INSERT INTO newstory (ntitle,nsub,newstory,nimage,newstype,newsdate,updatedon,mycolor,rid) VALUES(@ntitle,@nsub,@newstory,@nimage,@newstype,@updatedon,@updatedon,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("nsub", valDict["nsub"]);
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("relateNews") && !valDict.ContainsKey("nsub")) {
                            scmd.CommandText = "INSERT INTO newstory (ntitle,newstory,nimage,newstype,newsdate,updatedon,mycolor,rid) VALUES(@ntitle,@newstory,@nimage,@newstype,@updatedon,@updatedon,@mycolor,@rid)";
                            scmd.Parameters.AddWithValue("rid", valDict["relateNews"]);
                        } else if (valDict.ContainsKey("nsub") && !valDict.ContainsKey("relateNews")) {
                            scmd.CommandText = "INSERT INTO newstory (ntitle,nsub,newstory,nimage,newstype,newsdate,updatedon,mycolor) VALUES(@ntitle,@nsub,@newstory,@nimage,@newstype,@updatedon,@updatedon,@mycolor)";
                            scmd.Parameters.AddWithValue("nsub", valDict["nsub"]);
                        } else
                            scmd.CommandText = "INSERT INTO newstory (ntitle,newstory,nimage,newstype,newsdate,updatedon,mycolor) VALUES(@ntitle,@newstory,@nimage,@newstype,@updatedon,@updatedon,@mycolor)";
                        scmd.Parameters.AddWithValue("ntitle", valDict["title"]);
                        scmd.Parameters.AddWithValue("newstory", valDict["nstory"]);
                        scmd.Parameters.AddWithValue("mycolor", valDict["myColor"]);
                        scmd.Parameters.AddWithValue("newstype", "Newstory");
                        if (valDict["img"] != "") {
                            scmd.Parameters.AddWithValue("nimage", valDict["img"]);
                        } else
                            scmd.Parameters.AddWithValue("nimage", "No Image");
                        scmd.Parameters.AddWithValue("udatedon", dt);
                        scmd.Parameters.AddWithValue("newsdate", dt);
                        scmd.Prepare();
                        scmd.ExecuteNonQuery();
                        scmd.Parameters.Clear();
                        if (valDict["img"] != "") {
                            long lid = scmd.LastInsertedId;
                            scmd.CommandText = "INSERT INTO newsimages(imgurl,nsid,newsdate,updatedon) values (@imgurl,@nsid,@updatedon,@updatedon)";
                            scmd.Parameters.AddWithValue("imgurl", valDict["img"]);
                            scmd.Parameters.AddWithValue("nsid", lid);
                            scmd.Parameters.AddWithValue("udatedon", dt);
                            scmd.Parameters.AddWithValue("newsdate", dt);
                            scmd.Prepare();
                            scmd.ExecuteNonQuery();
                        }
                        res = true;
                    }
                }
            } catch (Exception ee) {
                res = false;
            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return res;
        }

        private static void updatePriority(Dictionary<string, string> valDict) {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["MyLocalDb"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            try {
                int pr = Convert.ToInt32(valDict["priority"]);
                scon.Open();
                scmd.Connection = scon;
                string query = "";

                if (pr == 1) {
                    query = " WHEN priority = '1' THEN '2' "
                    + " WHEN priority = '2' THEN '3'"
                    + " WHEN priority = '3' THEN '4'"
                    + " WHEN priority = '4' THEN '5'"
                    + " WHEN priority = '5' THEN '6'"
                    + " WHEN priority = '6' THEN '7' END)";
                }
                if (pr == 2)
                    query = " WHEN priority = '1' THEN '1' "
                    + " WHEN priority = '2' THEN '3'"
                    + " WHEN priority = '3' THEN '4'"
                    + " WHEN priority = '4' THEN '5'"
                    + " WHEN priority = '5' THEN '6'"
                    + " WHEN priority = '6' THEN '7' END)";
                if (pr == 3)
                    query = " WHEN priority = '1' THEN '1'  WHEN priority = '2' THEN '2'"
                    + " WHEN priority = '3' THEN '4'"
                    + " WHEN priority = '4' THEN '5'"
                    + " WHEN priority = '5' THEN '6'"
                    + " WHEN priority = '6' THEN '7' END)";
                if (pr == 4)
                    query = " WHEN priority = '1' THEN '1'"
                    + " WHEN priority = '2' THEN '2'"
                         + " WHEN priority = '3' THEN '3'"
                        + " WHEN priority = '4' THEN '5' "
                    + " WHEN priority = '5' THEN '6'"
                    + " WHEN priority = '6' THEN '7' END)";
                if (pr == 5)
                    query = " WHEN priority = '1' THEN '1'"
                    + " WHEN priority = '2' THEN '2'"
                    + " WHEN priority = '3' THEN '3'"
                    + " WHEN priority = '4' THEN '4' "
                    + " WHEN priority = '5' THEN '6'"
                    + " WHEN priority = '6' THEN '7' END)";
                if (pr == 6)
                    query =
                        " WHEN priority = '1' THEN '1'"
                        + " WHEN priority = '2' THEN '2'"
                         + " WHEN priority = '3' THEN '3'"
                        + " WHEN priority = '4' THEN '4' "
                        + " WHEN priority = '5' THEN '5'"
                        + " WHEN priority = '6' THEN '7 'END)";
                scmd.CommandText = "UPDATE impnews SET priority = (CASE " + query;
                /*priority = '1' THEN '2'"*/
                //+"WHEN priority = '2' THEN '3'"
                //+"WHEN priority = '3' THEN '4'"
                //+"WHEN priority = '4' THEN '5'"
                //+"WHEN priority = '5' THEN '6'"
                //+"WHEN priority = '6' THEN '7' END)";
                scmd.Prepare();
                scmd.ExecuteNonQuery();
            } catch (Exception ex) {

            } finally {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open) {
                    scon.Dispose();
                    scon.Close();
                }
            }
        }
    }
}