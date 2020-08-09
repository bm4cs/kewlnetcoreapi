// see previous example for the things that are not commented

const Provider = require('oidc-provider');
const jwks = require('./jwks.json');

const oidc = new Provider(`http://localhost:9090`, {
    clients: [
        // reconfigured the foo client for the purpose of showing the adapter working
        {
            client_id: 'foo',
            redirect_uris: ['https://bm4csarchbox.local:5001'],
            response_types: ['id_token'],
            grant_types: ['implicit'],
            token_endpoint_auth_method: 'none',
        },
    ],
    jwks,
    formats: {
        AccessToken: 'jwt',
    },
    features: {
        encryption: { enabled: true },
        introspection: { enabled: true },
        revocation: { enabled: true },
    },
});

// oidc.proxy = true;
// oidc.keys = process.env.SECURE_KEY.split(',');
const server = oidc.listen(9090, () => {
    console.log('oidc-provider listening on port 9090, check http://localhost:9090/.well-known/openid-configuration');
});