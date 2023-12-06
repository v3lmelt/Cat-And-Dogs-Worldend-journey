using System.Collections;
using NUnit.Framework;
using Tests.TestInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayTest
{
    public class MovementTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MovementTestWithEnumeratorPasses()
        {
            SceneManager.LoadScene("UnitTestScene", LoadSceneMode.Single);
            yield return null;
            yield return MoveLeft();
            yield return new WaitForSeconds(1);
            yield return MoveRight();
            yield return new WaitForSeconds(1);
            yield return Jump();
            yield return new WaitForSeconds(1);
            yield return Attack();
            yield return new WaitForSeconds(1);
            yield return RangedAttack();
            yield return new WaitForSeconds(1);
            Assert.IsNotNull(GameObject.Find("MagicBall"), "Magicball can't be found!");
            yield return Dash();
            yield return new WaitForSeconds(1);
        }
    
        private IEnumerator MoveLeft()
        {
            TestController.MoveLeft = true;
            yield return new WaitForSeconds(1);
            TestController.MoveLeft = false;
        }
    
        public IEnumerator MoveRight()
        {
            TestController.MoveRight = true;
            yield return new WaitForSeconds(1);
            TestController.MoveRight = false;
        }
            
        public IEnumerator Jump()
        {
            TestController.Jump = true;
            yield return null;
            TestController.Jump = false;
        }
            
        public IEnumerator Attack()
        {
            TestController.Attack = true;
            yield return null;
            TestController.Attack = false;
        }
            
        public IEnumerator RangedAttack()
        {
            TestController.RangedAttack = true;
            yield return new WaitForSeconds(1);
            TestController.RangedAttack = false;
        }
            
        public IEnumerator Dash()
        {
            TestController.Dash = true;
            yield return null;
            TestController.Dash = false;
        }
        
        private IEnumerator WaitForSceneLoad()
        {
            while (SceneManager.GetActiveScene().name != "UnitTestScene")
            {
                yield return null;
            }
        }
    }
}
