using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.Battle
{
    public class BuffManager : Singleton<BuffManager>
    {
        private IList<BuffBase> m_BuffList = new List<BuffBase>();

        public BuffManager()
        {

        }

        public BuffBase CreateBuff(BattleCard owner, int id, int bf_value)
        {
            BuffBase buff_base = new BuffGeneric(owner, id, bf_value);
            if (!buff_base.Init())
            {
                Debug.LogError("CreateBuff");
                return null;
            }

            if (!buff_base.LoadBuff())
            {
                Debug.LogError("CreateBuff");
                return null;
            }
			buff_base.UseBuff();
            m_BuffList.Add(buff_base);
            return buff_base;
        }

        public void DestroyBuff(BuffBase bf)
        {
            if (bf != null)
            {
                bf.Destroy();
                m_BuffList.Remove(bf);
            }
        }

        public void OnDestroy()
        {
            foreach (BuffBase bf in m_BuffList)
            {
                DestroyBuff(bf);
            }
            m_BuffList.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}

