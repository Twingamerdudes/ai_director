using UnityEngine;

namespace StageDirections
{
    public class MoveCurrentChar : StageDirection
    {
        public MoveCurrentChar()
        {
            name = "moveCurChar x y";
        }
        public override void Execute(string[] args, object[] utils)
        {
            StartCoroutine(GameObject.Find((string)utils[0]).GetComponent<Character>().MoveCharacterTo(float.Parse(args[0]), float.Parse(args[1])));
        }
    }
}
