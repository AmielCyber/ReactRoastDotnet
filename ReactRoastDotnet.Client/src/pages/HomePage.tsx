function HomePage() {
    return (
        <main className="flex z-0 flex-col justify-center items-center">
            <div className="hero-content mt-8 text-center">
                <div className="max-w-md text-gray-900 dark:text-white">
                    <h1 className="mb-4 text-3xl font-extrabold md:text-5xl lg:text-6xl">
                        Welcome to{" "}
                        <span className="text-transparent bg-clip-text bg-gradient-to-r from-primary to-secondary">
              React Coffee
            </span>{" "}
                    </h1>
                    <p className="py-4 text-xl">Order fresh coffee to go</p>
                    <button className="btn btn-primary">Order Now!</button>
                </div>
            </div>
        </main>
    );
}

export default HomePage;