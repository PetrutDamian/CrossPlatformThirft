package app.clientFX.Controllers;

import app.clientFX.Thrift.Cursa;
import app.clientFX.Thrift.Rezervare;
import app.clientFX.Thrift.ThriftObserver;
import app.clientFX.Thrift.ThriftUtils;
import org.apache.thrift.TException;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class Updater implements ThriftObserver.Iface {
    private MainController ctrl;
    @Override
    public void seatsDecremented(Cursa cursa) throws TException {
        ctrl.seatsDecremented(ThriftUtils.toModelCursa((cursa)));
        //app.model.Cursa cursa1 = new app.model.Cursa(cursa.id,cursa.destinatie,
        //        LocalDateTime.now(),cursa.locuriDisponibile);
       // ctrl.seatsDecremented(cursa1);
    }

    @Override
    public void newBookings(List<Rezervare> rezervari) throws TException {
        ctrl.newBookings(ThriftUtils.toModelRezervari(rezervari));
    }
    public void setCtrl(MainController ctrl){
        this.ctrl=ctrl;
    }

}
