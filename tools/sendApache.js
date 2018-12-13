var stompit = require( 'stompit' );

var connectOptions = {
    'host': 'localhost',
    'port': 61613,
    'connectHeaders': {
        'host': '/',
        'login': 'myconsumer',
        'passcode': 'consumerpassword',
        'heart-beat': '5000,5000'
    }
};

stompit.connect( connectOptions, function ( error, client ) {

    if ( error ) {
        console.log( 'connect error ' + error.message );
        return;
    }

    var sendHeaders = {
        'destination': '/topic/DadosRastreioPRODEST',
        'content-type': 'application/json'
    };
    console.log( "Enviando 100.000 mensagens..." )
    for ( var i = 0; i < 10; i++ ) {
        var frame = client.send( sendHeaders );
        frame.write( { chave: "valor" } );
        frame.end();
        if ( i % 1000 == 0 ) {
            console.log( "Mensagem " + i );
        }
    }
    client.disconnect();
} );
