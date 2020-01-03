genrule(
  name = "compass_zip",
  srcs = glob(["RoR2Compass/bin/Debug/netstandard2.0/RoR2Compass.dll", "README.md", "manifest.json", "icon.png"]),
  tools = ["@bazel_tools//tools/zip:zipper"],
  outs = ["ror2compass.zip"],
  cmd = "$(location @bazel_tools//tools/zip:zipper) cf $@ $(SRCS)",
)