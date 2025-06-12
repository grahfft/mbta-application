using System.ComponentModel;

[DataObject]
public class MbtaCache : IMbtaCache
{
    private IMbtaRepository mbtaRepository;

    public MbtaCache(IMbtaRepository mbtaRepository)
    {
        this.mbtaRepository = mbtaRepository;
    }
}