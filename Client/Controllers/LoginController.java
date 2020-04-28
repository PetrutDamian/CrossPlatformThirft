package app.clientFX.Controllers;

import app.clientFX.Thrift.ThriftObserver;
import app.clientFX.Thrift.ThriftService;
import app.model.User;
import app.clientFX.Utils.Utils;
import javafx.event.ActionEvent;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;
import org.apache.thrift.TException;
import org.apache.thrift.server.TServer;
import org.apache.thrift.server.TSimpleServer;
import org.apache.thrift.transport.TServerSocket;
import org.apache.thrift.transport.TServerTransport;
import org.apache.thrift.transport.TTransportException;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class LoginController {

    public TextField usernameField;
    public PasswordField passwordField;
    private ThriftService.Client server;
    private Stage myStage;
    private MainController parentCtrl;
    private Pane parentPane;
    private User user;
    private int tryPort;
    private Updater updater = new Updater();
    private TServerTransport getSocket() {
        while (true) {
            try {
               return new TServerSocket(tryPort);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
    private static void startUpdater(TServer server){
        server.serve();
    }
    public void handleLogin(ActionEvent actionEvent) {
        System.out.println("i got here");
        String username  = usernameField.getText();
        String password = passwordField.getText();
        user = new User(username,password);
        try{
            app.clientFX.Thrift.User usr = new app.clientFX.Thrift.User();
            usr.id = username;
            usr.password = password;
            tryPort++;
            TServerTransport socket = getSocket();

            boolean da = server.login(usr,tryPort);
            if(da) {

                ThriftObserver.Processor processor = new ThriftObserver.Processor(updater);
                updater.setCtrl(parentCtrl);
                TServer updateServer =  new TSimpleServer(new TServer.Args(socket).processor(processor));
                Runnable simple = new Runnable() {
                    @Override
                    public void run() {
                        startUpdater(updateServer);
                    }
                };
                new Thread(simple).start();
                System.out.println("Started update thread, port assigned:"+ tryPort);

                Stage stage = new Stage();
                stage.setTitle("User logged in: " + user.getId());
                stage.setScene(new Scene(parentPane, 850, 600));
                parentCtrl.setStage(stage);
                parentCtrl.setUser(user);
                parentCtrl.setPort(tryPort);
                myStage.close();
                parentCtrl.prepareToShow();
                stage.show();
            }else{
                Utils.displayMessage("Login Failed!\nIncorrect password or username!", Alert.AlertType.ERROR);
                passwordField.setText("");
            }
        }catch (TException ex){
            ex.printStackTrace();
        }
    }

    public void setServer(ThriftService.Client serv, Stage stage){
        Properties props = new Properties();
        InputStream inputStream = getClass().getClassLoader().
                getResourceAsStream("client.properties");
        try {
            props.load(inputStream);
            tryPort = Integer.parseInt(props.getProperty("port"));
        } catch (IOException e) {
            e.printStackTrace();
        }


        this.server = serv;
        this.myStage = stage;
        createMainController();
    }

    private void createMainController() {
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/Views/main_window.fxml"));
        try {
            AnchorPane pane = loader.load();
            MainController ctrl = loader.getController();
            ctrl.setService(this.server);
            parentCtrl = ctrl;
            parentPane = pane;
        } catch (IOException e) {
            e.printStackTrace();
        }

    }
}
