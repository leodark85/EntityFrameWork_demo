using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFramework_Demo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label1.Text = string.Empty;
            }
            GridViewReloadData();
        }

        private void GridViewReloadData()
        {
            using (CompanyEntities_2 ent = new CompanyEntities_2())
            {
                GridView1.DataSource = ent.Users.ToList();
                GridView1.DataBind();
               
            }
        }
        private void CleanControls ()
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (CompanyEntities_2 Ent = new CompanyEntities_2())
            {
                if (Label1.Text != string.Empty)
                {
                    int Id = int.Parse(Label1.Text);
                    var UserToEdit = Ent.Users.Where(R => R.IdUsuario == Id).First();
                    UserToEdit.Name = TextBox1.Text;
                    UserToEdit.email = TextBox2.Text;
                    UserToEdit.birthDate = TextBox3.Text;
                    Ent.SaveChanges();
                    CleanControls();
                 

                }
                else
                {
                    User Newuser = new User();
                    Newuser.Name = TextBox1.Text;
                    Newuser.email = TextBox2.Text;
                    Newuser.birthDate = TextBox3.Text;
                    Ent.Users.Add(Newuser);
                    Ent.SaveChanges();
                }
            }
            GridViewReloadData();
        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
            int idUser = int.Parse(GridView1.Rows[e.NewEditIndex].Cells[1].Text);
            using (CompanyEntities_2 ent = new CompanyEntities_2())
            {
                var UserToEdit= ent.Users.Where(R => R.IdUsuario == idUser).First(); ;
                Label1.Text = UserToEdit.IdUsuario.ToString();
                TextBox1.Text = UserToEdit.Name;
                TextBox2.Text = UserToEdit.email;
                TextBox3.Text = UserToEdit.birthDate;
                ent.SaveChanges();
            }
            GridViewReloadData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            int  idUser =int.Parse( GridView1.Rows[e.RowIndex].Cells[1].Text);
            using (CompanyEntities_2 ent = new CompanyEntities_2())
            {
                var  UserToDelete = ent.Users.Where(R => R.IdUsuario == idUser).First(); ;
                ent.Users.Remove(UserToDelete);
                ent.SaveChanges(); 
            }
            GridViewReloadData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }

       
    }