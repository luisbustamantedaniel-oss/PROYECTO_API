import { useEffect, useState } from "react";
import API from "../services/api";

function Risk() {
    const [risks, setRisks] = useState([]);

    useEffect(() => {
        API.get("/Risk")
            .then(res => setRisks(res.data))
            .catch(err => console.error(err));
    }, []);

    return (
        <div className="container mt-4">
            <h2>GeoRiskAI - Lista de Riesgos</h2>

            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>País</th>
                        <th>Nivel de Riesgo</th>
                    </tr>
                </thead>
                <tbody>
                    {risks.map((r, index) => (
                        <tr key={index}>
                            <td>{r.country}</td>
                            <td>{r.riskLevel}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Risk;
