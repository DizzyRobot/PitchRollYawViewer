using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using STL_Tools;
using OpenTK.Graphics.OpenGL;
using BatuGL;

namespace PitchRollYawViewer
{
    public partial class AppMainForm : Form
    {
        bool monitorLoaded = false;
        bool moveForm = false;
        int moveOffsetX = 0;
        int moveOffsetY = 0;
        Batu_GL.VAO_TRIANGLES modelVAO = null; // 3d model vertex array object
        private SerialPitchRollYaw serialMonitor;
        Vector3 minPos = new Vector3();
        Vector3 maxPos = new Vector3();
        private float kScaleFactor = 5.0f;
        private PRY orientation = new PRY { Pitch = 90f };

        public AppMainForm()
        {
            /* dot/comma selection for floating point numbers */
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();

            GL_Monitor.MouseWheel += (s, e) => kScaleFactor *= (e.Delta > 0 ? 1.1f : 0.9f);


            serialMonitor = new SerialPitchRollYaw();
            serialMonitor.NewPorts += SerialMonitor_NewPorts;
            serialMonitor.NewData += SerialMonitor_NewData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Batu_GL.Configure(GL_Monitor, Batu_GL.Ortho_Mode.CENTER);
            serialMonitor.CheckForNewPorts();
            LoadStlFile("model.stl");
        }

        private void SerialMonitor_NewData(object sender, PRY e)
        {
            this.orientation = e;

            this.Invoke(() => 
            {
                this.lblPitch.Text = $"Pitch: {e.Pitch}";
                this.lblRoll.Text = $"Roll: {e.Roll}";
                this.lblYaw.Text = $"Yaw: {e.Yaw}";
            });
        }

        private void SerialMonitor_NewPorts(object sender, string[] e)
        {
            this.cmbPort.Items.Clear();
            this.cmbPort.Items.AddRange(e);
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            GL_Monitor.Invalidate();
            if (moveForm)
            {
                this.SetDesktopLocation(MousePosition.X - moveOffsetX, MousePosition.Y - moveOffsetY);
            }
        }

        private void GL_Monitor_Load(object sender, EventArgs e)
        {
            GL_Monitor.AllowDrop = true;
            monitorLoaded = true;
            GL.ClearColor(Color.Black);
        }

        private void ConfigureBasicLighting(Color modelColor)
        {
            float[] light_1 = new float[] {
            0.2f * modelColor.R / 255.0f,
            0.2f * modelColor.G / 255.0f,
            0.2f * modelColor.B / 255.0f,
            1.0f };
            float[] light_2 = new float[] {
            10.0f * modelColor.R / 255.0f,
            10.0f * modelColor.G / 255.0f,
            10.0f * modelColor.B / 255.0f,
            1.0f };
            float[] specref = new float[] { 
                0.2f * modelColor.R / 255.0f, 
                0.2f * modelColor.G / 255.0f, 
                0.2f * modelColor.B / 255.0f, 
                1.0f };
            float[] specular_0 = new float[] { -1.0f, -1.0f, 1.0f, 1.0f };
            float[] specular_1 = new float[] { 1.0f, -1.0f, 1.0f, 1.0f };
            float[] lightPos_0 = new float[] { 1000f, 1000f, -200.0f, 0.0f };
            float[] lightPos_1 = new float[] { -1000f, 1000f, -200.0f, 0.0f };

            GL.Enable(EnableCap.Lighting);
            /* light 0 */
            GL.Light(LightName.Light0, LightParameter.Ambient, light_1);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_2);
            GL.Light(LightName.Light0, LightParameter.Specular, specular_0);
            GL.Light(LightName.Light0, LightParameter.Position, lightPos_0);
            GL.Enable(EnableCap.Light0);
            /* light 1 */
            GL.Light(LightName.Light1, LightParameter.Ambient, light_1);
            GL.Light(LightName.Light1, LightParameter.Diffuse, light_2);
            GL.Light(LightName.Light1, LightParameter.Specular, specular_1);
            GL.Light(LightName.Light1, LightParameter.Position, lightPos_1);
            GL.Enable(EnableCap.Light1);
            /*material settings  */
            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, specref);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 10);
            GL.Enable(EnableCap.Normalize);
        }

        private void GL_Monitor_Paint(object sender, PaintEventArgs e)
        {
            if (!monitorLoaded)
                return;

            Batu_GL.Configure(GL_Monitor, Batu_GL.Ortho_Mode.CENTER);
            if (modelVAO != null) ConfigureBasicLighting(modelVAO.color);
            //GL.Translate(orb.PanX, orb.PanY, 0);
            //GL.Rotate(orb.orbitStr.angle, orb.orbitStr.ox, orb.orbitStr.oy, orb.orbitStr.oz);

            GL.Rotate(orientation.Yaw, 0.0f, 1.0f, 0.0f);
            GL.Rotate(orientation.Pitch, 1.0f, 0.0f, 0.0f);
            GL.Rotate(orientation.Roll, 0.0f, 0.0f, 1.0f);

            GL.Scale(kScaleFactor, kScaleFactor, kScaleFactor); // small multiplication factor to scaling
            GL.Translate(-minPos.x, -minPos.y, -minPos.z);
            GL.Translate(-(maxPos.x - minPos.x) / 2.0f, -(maxPos.y - minPos.y) / 2.0f, -(maxPos.z - minPos.z) / 2.0f);
            if (modelVAO != null) modelVAO.Draw();
            //Batu_GL.Draw_WCS();

            GL_Monitor.SwapBuffers();
        }

        private void LoadStlFile(string fileName)
        {
            STLReader stlReader = new STLReader(fileName);
            TriangleMesh[] meshArray = stlReader.ReadFile();
            modelVAO = new Batu_GL.VAO_TRIANGLES();
            modelVAO.parameterArray = STLExport.Get_Mesh_Vertices(meshArray);
            modelVAO.normalArray = STLExport.Get_Mesh_Normals(meshArray);
            modelVAO.color = Color.Green;
            minPos = stlReader.GetMinMeshPosition(meshArray);
            maxPos = stlReader.GetMaxMeshPosition(meshArray);

            orientation = new PRY { Pitch = 90f };
            kScaleFactor = 5f;

            if (stlReader.Get_Process_Error())
            { 
                modelVAO = null;
                /* if there is an error, deinitialize the gl monitor to clear the screen */
                Batu_GL.Configure(GL_Monitor, Batu_GL.Ortho_Mode.CENTER);
                GL_Monitor.SwapBuffers();
            }
        }

        private void cmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialMonitor.Connect((string)cmbPort.SelectedItem);
        }
    }
}