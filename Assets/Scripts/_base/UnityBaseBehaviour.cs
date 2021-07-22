using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts._base
{
    public class UnityBaseBehaviour : MonoBehaviour
    {
        #region Empty Virtuals

        public virtual void Start()
        {}

        public virtual void Awake()
        { }

        public virtual void Update()
        {}

        public virtual void FixedUpdate()
        {}

        public virtual void OnCollisionEnter2D(Collision2D c)
        {}

        public virtual void OnMouseUpAsButton()
        {}

        public virtual void OnMouseOver()
        {}

        public virtual void OnMouseExit()
        { }

        #endregion

        public T InstantiateIfExists<T>(T original, Vector3 position, Quaternion rotation) where T : Object
        {
            return InstantiateIf<T>(original, position, rotation, original != null);    
        }
        
        public T InstantiateIf<T>(T original, Vector3 position, Quaternion rotation, bool condition = true) where T : Object
        {
            if(condition)
                return (T)Instantiate(original, position, rotation);
            return default(T);
        }

        public void InvokeRepeating(Expression<Action> method, float time, float repeatRate)
        {            
            InvokeRepeating(GetMemberName(method), time, repeatRate);
        }

        public void Invoke(Expression<Action> method, float time)
        {            
            Invoke(GetMemberName(method), time);
        }

        /// <summary> 
        /// Helper method to get member name with compile time verification to avoid typo. 
        /// </summary> 
        /// <param name="expr">The lambda expression usually in the form of o => o.member.</param> 
        /// <returns>The name of the property.</returns> 
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Not used in all design time assemblies.")]
        private static string GetMemberName(Expression<Action> expr)
        {
            Expression body = expr.Body;
            if (body is MethodCallExpression )
            {
                var methodCallExpression = body as MethodCallExpression;
                return methodCallExpression.Method.Name;
            }
            if (body is MemberExpression || body is UnaryExpression)
            {
                MemberExpression memberExpression = body as MemberExpression ?? (MemberExpression)((UnaryExpression)body).Operand;
                return memberExpression.Member.Name;
            }            
            return expr.ToString();
        }
    }
}