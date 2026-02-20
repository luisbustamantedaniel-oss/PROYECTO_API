import React, { useState } from "react";

function App() {
    const [country, setCountry] = useState("");
    const [data, setData] = useState(null);

    const getCountry = async () => {
        if (!country) return;
        try {
            const res = await fetch(`http://localhost:5000/api/risk/${country}`);
            const json = await res.json();
            setData(json);
        } catch {
            setData({ error: "Error consultando la API" });
        }
    };

    return (
        <div>
            <h1>GeoRisk AI – Demo Parcial</h1>
            <input
                value={country}
                onChange={(e) => setCountry(e.target.value)}
                placeholder="País"
            />
            <button onClick={getCountry}>Cargar</button>

            {data && !data.error && (
                <div>
                    <p>Country: {data.country}</p>
                    <p>Region: {data.region}</p>
                    <p>{data.summary}</p>
                </div>
            )}

            {data && data.error && <p>{data.error}</p>}
        </div>
    );
}

export default App;
