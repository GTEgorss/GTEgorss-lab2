package com.example.demo;

import org.springframework.web.bind.annotation.*;

@RestController
public class ZxcController {
    @GetMapping("/zxc")
    public Zxc getZxc(@RequestParam int id) {
        return new Zxc(id);
    }

    @PostMapping("/zxc")
    public Zxc newZxc(@RequestBody Zxc newZxc) {
        return newZxc;
    }
}
