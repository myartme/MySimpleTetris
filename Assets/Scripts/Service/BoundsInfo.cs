using UnityEngine;

namespace Service
{
    public static class BoundsInfo
    {
        public static Bounds GetAllBoundsCollapse(GameObject gameObject)
        {
            var bounds = GetObjectBounds(gameObject);
            var childrenTransform = gameObject.transform;

            for (var i = 0; i < childrenTransform.childCount; i++)
            {
                var child = childrenTransform.GetChild(i);
                if (child.childCount == 0)
                {
                    if (i == 0)
                    {
                        bounds = GetObjectBounds(child.gameObject);
                    }
                    else
                    {
                        bounds.Encapsulate(GetObjectBounds(child.gameObject));
                    }
                }
                else
                {
                    bounds.Encapsulate(GetAllBoundsCollapse(child.gameObject));
                }
            }

            return bounds;
        }

        private static Bounds GetObjectBounds(GameObject gameObject)
        {
            var renderer = gameObject.GetComponent<Renderer>();
            if(!renderer)
                return new Bounds(gameObject.transform.position,Vector3.zero);
            
            return renderer.bounds;
        }
    }
}