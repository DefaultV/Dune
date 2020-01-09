using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class update_hologram_pieces : MonoBehaviour
{
    public GameObject player;
    public GameObject player_piece;
    public GameObject vehicle;
    public GameObject vehicle_piece;
    public GameObject cave;
    public GameObject cave_piece;
    LineRenderer lr;
    void Start(){
        lr = GetComponent<LineRenderer>();
    }

    void Update(){
        updatePieces();
        updateLine();
    }

    void updatePieces() {
        //player_piece.transform.localRotation = Camera.main.transform.localRotation;
        Vector3 vec_ply2vhcl = (player.transform.position - vehicle.transform.position).normalized;
        float dist_ply2vhcl = Vector3.Distance(player.transform.position, vehicle.transform.position);
        vehicle_piece.transform.localPosition = -vec_ply2vhcl * (dist_ply2vhcl * 0.2f);
    }

    void updateLine() {
        lr.SetPosition(0, player_piece.transform.localPosition);
        lr.SetPosition(1, vehicle_piece.transform.localPosition);
    }
}
