using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Factory_VR;
using UnityEngine.EventSystems;


namespace Factory_VR
{
    public class TesteRobô : MonoBehaviour
    {
        public Robot MH24 = new Robot("MH24", 6);
        public float[] ZeroJoint = { 0,0,-90,0,0,0,0};
        //private int i = 0;
        public float speed = 2;
        public int flag1 = 0;
        public int step = 0;
        public int j = 0;
        public int test = 0;
        public float[] Angles = { 0, 0, 0, 0, 0, 0, 0 };
        public int Mode = 0;
        public GameObject GravaPonto;

        // Start is called before the first frame update
        void Start()
        {
            MH24.RobotInitiate(ZeroJoint);  
        }

        // Update is called once per frame
        void Update()
        {
            
            MH24.Selection();
            MH24.Manipulate();
            
            switch(Mode)
            { 
                case 1:

                    if ( step != -1)//Se não é o fim do programa
                    {
                        if (step == 1) //Se chegou no fim da linha vai pra p´roxima
                        {
                            j++;
                            step = 0;
                        }
                        Debug.Log(j);
                        step = MH24.SetRobotToPosition(j, speed);
                        // if (flag1 == 0)
                        //{
                        //    test = MH24.SetRobotToPositionNOW(j + 1);
                        //     flag1 = 1;
                        // }
                        //else
                        //  step = MH24.SetRobotToPosition(j + 1, speed);
                    }
                    else
                    {
                        Mode = 0;
                        j = 0;
                        step = 0;
                    }
                        
                    break;

                case 2:
                    MH24.SetRobotToJointAngle(Angles);
                    break;



            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                if (Mode == 0)
                    Mode = 2;
                else
                    Mode = 0;
            }


            if (Input.GetKeyDown(KeyCode.K))
            {
                CreateProgram();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PrintJoint();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PrintJointTest();
            }

            if (Input.GetKeyDown(KeyCode.E))
                PlayPorgram();


        }

        public void PlayPorgram()
        {
            Mode = 1;
            MH24.LoadProgram("P5");
        }

        public void SetPos()
        {
            Mode = 1;
        }

        public void CreateProgram()
        {
            MH24.CreateProgram(@"\P5.txt");
        }

        public void PrintJointTest()
        {
            MH24.PrintJointTest();
        }

        public void PrintJoint()
        {
            MH24.PrintJointPosition();
            MH24.WriteProgram(MH24.GetInstantJointPoint(), @"\P5.txt");
            //Debug.Log(MH24.GetInstantJointPoint());  
            // MH24.WriteProgram(MH24.GetInstantJointPoint());
        }


        void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 24;

            GUI.Label(new Rect(10, 50, 0, 0), MH24.PrintJointPosition(), style);
        }
    }
}

