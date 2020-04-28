package app.clientFX;


import app.clientFX.Controllers.LoginController;
import app.clientFX.Thrift.ThriftService;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import org.apache.thrift.protocol.TBinaryProtocol;
import org.apache.thrift.protocol.TProtocol;
import org.apache.thrift.transport.TSocket;
import org.apache.thrift.transport.TTransport;

public class StartClientFX extends Application {
    public static void main(String[] args) {
        launch(args);
    }


    @Override
    public void start(Stage primaryStage) throws Exception {
        TTransport t = new TSocket("localhost",9090);
        t.open();
        TProtocol protocol = new TBinaryProtocol(t);
        ThriftService.Client server = new ThriftService.Client(protocol);

        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/Views/login.fxml"));
        AnchorPane rootPane = loader.load();

        LoginController ctrl =loader.getController();
        ctrl.setServer(server,primaryStage);

        primaryStage.setTitle("Login");
        primaryStage.setScene(new Scene(rootPane,500,600));
        primaryStage.show();


    }
}
