public class MbtaService : IMbtaService
{
    private IMbtaCache mbtaCache;

    public MbtaService(IMbtaCache mbtaCache)
    {
        this.mbtaCache = mbtaCache;
    }
}