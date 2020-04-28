package app.clientFX.Thrift;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

public class ThriftUtils {
    public static List<app.model.Cursa> toModelCurse(List<app.clientFX.Thrift.Cursa> curse){
        List<app.model.Cursa> all = new ArrayList<>();
        curse.stream().forEach(x->{

            LocalDateTime date = LocalDateTime.parse(x.date,DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm"));
            all.add(new app.model.Cursa(x.id,x.destinatie, date,x.locuriDisponibile));
        });
        return all;
    }
    public static List<app.model.Rezervare> toModelRezervari(List<app.clientFX.Thrift.Rezervare> rezervari){
        List<app.model.Rezervare> all = new ArrayList<>();
        rezervari.stream().forEach(x->{
            all.add(new app.model.Rezervare(x.id,x.idCursa,x.nrLoc,x.client));
        });
        return all;
    }
    public static List<app.clientFX.Thrift.Rezervare> fromNative(List<app.model.Rezervare> rezervari){
        List<Rezervare> all = new ArrayList<>();
        rezervari.stream().forEach(x->{
            Rezervare r = new Rezervare();
            r.id = x.getId();
            r.idCursa = x.getIdCursa();
            r.client = x.getClient();
            r.nrLoc = x.getNrLoc();
            all.add(r);
        });
        return all;
    }
    public static app.model.Cursa toModelCursa(Cursa cursa){
        return new app.model.Cursa(cursa.id,cursa.destinatie,LocalDateTime.now(),cursa.locuriDisponibile);
    }
}
