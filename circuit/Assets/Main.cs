using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    public GameObject cam;
    public float rotateSpeed=2;
    public float moveSpeedXY= 0.5f;
    public float moveSpeedXMouse=0.5f;
    public float moveSpeedYMouse= 0.5f;
    public float moveSpeedZ = 0.5f;
    public float snapGap;
    public GameObject wire, power, resistance,capacitance,inductor, intersection,voltmeter,ammeter,wireNet;
    Vector2 mouseLastPos;
    
    public List< electronicComponent> selectedList;
    void Start () {
        selectedList = new List<electronicComponent>();
	}
	Vector3 mousePos(float y,Vector2 pos)
    {
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(pos);
        Plane plane = new Plane(new Vector3(0, 1, 0), new Vector3(0, y, 0));
        float enter;
        plane.Raycast(ray, out enter);
        return ray.origin + ray.direction * enter;
    }
	void Update () {
        #region cam control
        float rx, ry;
        rx = cam.transform.rotation.eulerAngles.x;
        ry = cam.transform.rotation.eulerAngles.y;
        if (Input.GetMouseButton(1))
        {
            rx -= Input.GetAxis("Mouse Y") * rotateSpeed;
            ry += Input.GetAxis("Mouse X") * rotateSpeed;
        }
        cam.transform.rotation = Quaternion.Euler(rx, ry, 0);
        Vector3 camMove = new Vector3(Input.GetAxis("Horizontal") * moveSpeedXY - ((Input.GetMouseButton(2)) ? Input.GetAxis("Mouse X") : 0) * moveSpeedXMouse, -((Input.GetMouseButton(2)) ? Input.GetAxis("Mouse Y") : 0) * moveSpeedYMouse, Input.GetAxis("Vertical") * moveSpeedXY + Input.GetAxis("Mouse ScrollWheel") * moveSpeedZ);
        cam.transform.Translate(camMove);
        #endregion
        
        if (Input.GetMouseButtonDown(0))//select
        {
            mouseLastPos = Input.mousePosition;
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                electronicComponent curSelect = hit.collider.gameObject.GetComponent<electronicComponent>();
                if (curSelect != null)
                {
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        if (selectedList.Contains(curSelect))
                        {
                            curSelect.unselect();
                            selectedList.Remove(curSelect);
                        }
                        else
                        {
                            curSelect.select();
                            selectedList.Add(curSelect);
                        }
                    }
                     

                    else
                    {
                        foreach (electronicComponent c in selectedList) c.unselect();
                        selectedList.Clear();
                        //select a wireNet
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            if(curSelect.GetType() == typeof(Wire))
                            {
                                foreach(electronicComponent c in ((Wire)curSelect).wireNet.wires)
                                {
                                    c.select();
                                    selectedList.Add(c);
                                }
                                foreach (electronicComponent c in ((Wire)curSelect).wireNet.intersections)
                                {
                                    c.select();
                                    selectedList.Add(c);
                                }
                            }
                            if (curSelect.GetType() == typeof(Intersection))
                            {
                                foreach (electronicComponent c in ((Intersection)curSelect).wireNet.wires)
                                {
                                    c.select();
                                    selectedList.Add(c);
                                }
                                foreach (electronicComponent c in ((Intersection)curSelect).wireNet.intersections)
                                {
                                    c.select();
                                    selectedList.Add(c);
                                }
                            }
                        }
                        else
                        {
                            curSelect.select();
                            selectedList.Add(curSelect);
                        }
                    }
                }
                else
                {
                    foreach (electronicComponent c in selectedList) c.unselect();
                    selectedList.Clear();
                }
            }
            else
            {
                foreach (electronicComponent c in selectedList) c.unselect();
                selectedList.Clear();
            }
        }
        if (Input.GetMouseButton(0))//drag
        {
            foreach (electronicComponent c in selectedList)
            {
                c.move(mousePos(selectedList[selectedList.Count-1].gameObject.transform.position.y, Input.mousePosition) - mousePos(selectedList[selectedList.Count - 1].gameObject.transform.position.y, mouseLastPos),Input.GetKey(KeyCode.LeftAlt)?0:snapGap);
            }
        }
        if (Input.GetMouseButtonUp(0))//drag
        {
            foreach (electronicComponent c in selectedList)
            {
                c.OnMouseRelease();
            }
        }

        #region create/delete components
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (electronicComponent c in selectedList) { Data.Remove(c); c.delete(); }
            selectedList.Clear();
            reArrangeWireNets();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            InitializeComponent(Instantiate(power));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitializeComponent(Instantiate(resistance));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            InitializeComponent(Instantiate(capacitance));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            InitializeComponent(Instantiate(inductor));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject newObject = Instantiate(intersection);
            Intersection script = (Intersection)newObject.GetComponent<electronicComponent>();
            newObject.transform.position = mousePos(0, Input.mousePosition);
            script.create(this); 
            foreach (electronicComponent c in selectedList) c.unselect();
            selectedList.Clear();
            newObject.GetComponent<electronicComponent>().select();
            selectedList.Add(newObject.GetComponent<electronicComponent>());
            reArrangeWireNets();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (selectedList.Count == 2) {
                bool t=true;
                foreach(electronicComponent c in selectedList)
                {
                    if (c.n_emptyConnect() < 1) t = false;
                }
                if (t)
                {
                    GameObject newObject = Instantiate(wire);
                    newObject.GetComponent<electronicComponent>().create(this);
                    Wire wireScript = newObject.GetComponent<Wire>();
                    
                    wireScript.addConnection( selectedList[0]);
                    wireScript.addConnection(selectedList[1]);
                    selectedList[0].addConnection(wireScript);
                    selectedList[1].addConnection(wireScript);
                    foreach (electronicComponent c in selectedList) c.unselect();
                    selectedList.Clear();
                    newObject.GetComponent<electronicComponent>().select();
                    selectedList.Add(newObject.GetComponent<electronicComponent>());
                    reArrangeWireNets();
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (selectedList.Count == 2)
            {
                bool t = true;
                foreach (electronicComponent c in selectedList)
                {
                    if (c.GetType()!=typeof(Wire)) t = false;
                }
                if (t)
                {
                    GameObject newObject = Instantiate(voltmeter);
                    Voltmeter script = newObject.GetComponent<Voltmeter>();
                    script.create_(this, (Wire)selectedList[0], (Wire)selectedList[1]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (selectedList.Count == 1)
            {
                if (selectedList[0].GetType()!=typeof(Wire))
                {
                    GameObject newObject = Instantiate(ammeter);
                    Ammeter script = newObject.GetComponent<Ammeter>();
                    script.create_(this, selectedList[0]);
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            foreach (WireNet c in FindObjectsOfType<WireNet>())
            {
                c.Reset();
            }
            foreach (Capacitance c in FindObjectsOfType<Capacitance>())
            {
                c.Reset();
            }
        }

            #endregion

        mouseLastPos = Input.mousePosition;
    }

    void InitializeComponent(GameObject newObject)
    {
        Data.Add(newObject.GetComponent<electronicComponent>());
        newObject.transform.position = mousePos(0, Input.mousePosition);
        newObject.GetComponent<electronicComponent>().create(this);
        foreach (electronicComponent c in selectedList) c.unselect();
        selectedList.Clear();
        newObject.GetComponent<electronicComponent>().select();
        selectedList.Add(newObject.GetComponent<electronicComponent>());
    }

    List<electronicComponent> found;
    public void reArrangeWireNets()
    {
        foreach(WireNet wn in Data.wireNet)
        {
            Destroy(wn.gameObject);
        }
        Data.wireNet.Clear();
        List<electronicComponent> list = new List<electronicComponent>();
        found = new List<electronicComponent>();
        list.AddRange(FindObjectsOfType<Wire>());//-------add to data
        list.AddRange(FindObjectsOfType<Intersection>());//----------
        while (list.Count > 0)
        {
            electronicComponent cur = list[0];
            WireNet w = Instantiate(wireNet).GetComponent<WireNet>();
            Data.Add(w);
            foreach(electronicComponent c in getWireNet(cur))
            {
                if (c.GetType() == typeof(Wire))
                {
                    w.wires.Add((Wire)c);
                    ((Wire)c).wireNet = w;
                }
                if (c.GetType() == typeof(Intersection))
                {
                    w.intersections.Add((Intersection)c);
                    ((Intersection)c).wireNet = w;
                }
                list.Remove(c);
            }
            list.Remove(cur);
        }
    }
    public List<electronicComponent> getWireNet(electronicComponent c)
    {
        List<electronicComponent> result = new List<electronicComponent>();
        if (found.Contains(c)) return result;
        found.Add(c);
        if (c != null)
        {
            if (c.GetType() == typeof(Wire) || c.GetType() == typeof(Intersection))
            {
                result.Add(c);
                foreach (electronicComponent c1 in c.connect)
                {
                    result.AddRange(getWireNet(c1));
                }
            }
        }
        return result;
    }
}
