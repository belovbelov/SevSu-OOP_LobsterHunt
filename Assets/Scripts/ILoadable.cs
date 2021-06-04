using System.Collections;

namespace Lobster
{
    public interface ILoadable
    {
        public IEnumerator LoadLevel(int levelIndex);
    }
}