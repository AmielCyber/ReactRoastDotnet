interface ProblemDetails {
    type?: string;
    title: string;
    status: number;
    detail?: string;
    traceId?: string;
    errors?: Record<string, string[]>
}

export default ProblemDetails;
